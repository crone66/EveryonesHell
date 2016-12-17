using SFML.System;
using System;
using SFML.Graphics;

namespace EveryonesHell.EntityManagment
{
    public class Projectile : InteractiveObject
    {
        private Vector2f spawnPosition;
        private int damage = 10;
        private int maxDistance;
        private int distanceTravelled;
        private int spawnerId;
        private float damageReductionRate;
        private float teamDamageReductionRate;

        private bool doTeamDamage;

        public float DamageReductionRate
        {
            get
            {
                return damageReductionRate;
            }

            set
            {
                if (value >= 0 && value <= 1)
                    damageReductionRate = value;
                else
                    throw new ArgumentOutOfRangeException("damageReductionRate", "Value must be between zero and one!");
            }
        }

        public float TeamDamageReductionRate
        {
            get
            {
                return teamDamageReductionRate;
            }

            set
            {

                if (value >= 0 && value <= 1)
                    teamDamageReductionRate = value;
                else
                    throw new ArgumentOutOfRangeException("teamDamageReductionRate", "Value must be between zero and one!");
            }
        }

        public event EventHandler<VictimArgs> OnDoDamage;
        

        public Projectile(Vector2i size, AnimationManager animation, bool isMoveAble, float speed, int maxDistance, int groupId)
            :base(size, animation, isMoveAble, speed, null, groupId, -1)
        {
            doTeamDamage = false;
            teamDamageReductionRate = 0f;
            damageReductionRate = 0f;
            FactionId = -1;
            this.maxDistance = maxDistance;
        }

        public void Init(Vector2f sourcePosition, Vector2i sourceSize, Vector2f viewDirection, int factionId, int spawnerId)
        {
            this.spawnerId = spawnerId;
            FactionId = factionId;
            Vector2f spawn = CalculateSpawnPosition(viewDirection, sourceSize);         
            Vector2f offset = SetupRotation(viewDirection, sourceSize);
            Vector2f rotatedSize = GetRotatedSize(viewDirection);         
            spawn += sourcePosition + offset;

            Position = spawn;
            spawnPosition = Position;
            ViewDirection = viewDirection;
            OnCollision += InteractiveObject_OnCollision;
            BoundingBox = new IntRect(BoundingBox.Left, BoundingBox.Top, (int)rotatedSize.X, (int)rotatedSize.Y);
        }

        private Vector2f SetupRotation(Vector2f viewDirection, Vector2i sourceSize)
        {
            Vector2f rotatedSize = GetRotatedSize(viewDirection);
            Vector2f offset = new Vector2f((sourceSize.X / 2) - (rotatedSize.X / 2), (sourceSize.Y / 2) - (rotatedSize.Y / 2));

            if (viewDirection.X > 0)
                Rotation = 180f;
            else if (viewDirection.Y > 0)
                Rotation = 270f;
            else if (viewDirection.Y < 0)
                Rotation = 90f;
            else
                Rotation = 0;

            return offset;
        }

        private Vector2f CalculateSpawnPosition(Vector2f viewDirection, Vector2i sourceSize)
        {
            Vector2f spawn = new Vector2f(0, 0);
            Vector2f rotatedSize = GetRotatedSize(viewDirection);
            float offset = 0;
            if (viewDirection.X > 0)
                spawn.X = viewDirection.X * (sourceSize.X + rotatedSize.X + offset);
            else if (viewDirection.X < 0)
                spawn.X = viewDirection.X * (rotatedSize.X + offset);
            if (viewDirection.Y > 0)
                spawn.Y = viewDirection.Y * (sourceSize.Y + rotatedSize.Y + offset);
            else if (viewDirection.Y < 0)
                spawn.Y = viewDirection.Y * (rotatedSize.Y + offset);

            return spawn;
        }

        private Vector2f GetRotatedSize(Vector2f viewDirection)
        {
            if (viewDirection.Y != 0)
            {
                return new Vector2f(Size.Y, Size.X);
            }
            else
            {
                return new Vector2f(Size.X, Size.Y);
            }
        }

        private void CheckDistance()
        {
            Vector2f distance = Position - spawnPosition;
            if (distance.X != 0)
            {
                if(distance.X < 0)
                    distance.X *= -1;

                distanceTravelled = (int)Math.Floor(distance.X);
            }
            else if (distance.Y != 0)
            {
                if(distance.Y < 0)
                    distance.Y *= -1;

                distanceTravelled = (int)Math.Floor(distance.Y);
            }


            if (distanceTravelled >= maxDistance)
                CallOnDestroyEvent();
        }

        public override void Update(float elapsedSeconds)
        {
            Vector2f lastPosition = Position;
            base.Update(elapsedSeconds);
            Velocity = ViewDirection * Speed;

            CheckDistance();
        }

        public override void Draw(RenderWindow window)
        {
            CurrentSprite.Origin = new Vector2f(Size.X / 2, Size.Y / 2);
            base.Draw(window);
        }

        private void InteractiveObject_OnCollision(object sender, CollisionArgs e)
        {
            Entity otherObject = null;
            if (e.CollisionObjectDest != null && e.CollisionObjectDest != this)
                otherObject = e.CollisionObjectDest;
            else if (e.CollisionObjectSource != null && e.CollisionObjectSource != this)
                otherObject = e.CollisionObjectSource;

            if (otherObject != null)
            {
                if (otherObject is InteractiveObject)
                {
                    e.CancelBackward = true;
                    if (otherObject.Id != spawnerId)
                    {
                        InteractiveObject interactiveObject = otherObject as InteractiveObject;
                        if (doTeamDamage || FactionId != interactiveObject.FactionId)
                        {
                            int newHealth = interactiveObject.ChangeHealth(CalculateDamage(FactionId == interactiveObject.FactionId), this);
                            OnDoDamage?.Invoke(this, new VictimArgs(interactiveObject, damage, newHealth <= 0));
                        }
                        OnCollision -= InteractiveObject_OnCollision;
                    }
                }
            }

            if(otherObject == null || otherObject.Id != spawnerId)
                CallOnDestroyEvent();
        }

        private int CalculateDamage(bool teamAttack)
        {
            int currentDamage = damage * -1;
            if(DamageReductionRate != 0)
            {
                currentDamage = Convert.ToInt32(currentDamage + (((float)distanceTravelled / maxDistance) * (damage * DamageReductionRate)));
            }

            if(teamAttack)
                currentDamage -= Convert.ToInt32(currentDamage * teamDamageReductionRate);         

            return currentDamage;
        }

        public override Entity Clone()
        {
            return new Projectile(Size, animations.Clone(), IsMoveAble, Speed, maxDistance, GroupID);
        }
    }
}
