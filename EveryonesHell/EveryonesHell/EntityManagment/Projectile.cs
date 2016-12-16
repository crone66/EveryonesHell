using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveryonesHell.EntityManagment
{
    public class Projectile : InteractiveObject
    {
        private int damage = 10;
        private int maxDistance;
        private float damageReductionRate;

        private bool doTeamDamage;
        private int teamId;

        private bool doAreaDamage;
        private int areaDamageRadius;
        private float aoeDamageReductionRate;

        public event EventHandler<VictimArgs> OnDoDamage;
        

        public Projectile(Vector2i size, AnimationManager animation, bool isMoveAble, float speed):
            base(size, animation, isMoveAble, speed, null)
        {
        }

        public void Init(Vector2f sourcePosition, Vector2i sourceSize, Vector2f viewDirection)
        {
            Vector2f spawn = new Vector2f(0, 0);
            if (viewDirection.X != 0)
                spawn.X = viewDirection.X * (sourceSize.X + Size.X);

            if (viewDirection.Y != 0)
                spawn.Y = viewDirection.Y * (sourceSize.Y + Size.Y);

            spawn += sourcePosition;
            Position = spawn;
            ViewDirection = viewDirection;

            if (viewDirection.X > 0)
                Rotation = 180f;
            else if (viewDirection.Y > 0)
                Rotation = 270f;
            else if (viewDirection.Y < 0)
                Rotation = 90f;
            else
                Rotation = 0;
        }

        public override void Update(float elapsedSeconds)
        {
            base.Update(elapsedSeconds);
            Velocity = ViewDirection * Speed;
        }

        protected override void InteractiveObject_OnCollision(object sender, CollisionArgs e)
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
                    InteractiveObject interactiveObject = otherObject as InteractiveObject;
                    int newHealth = interactiveObject.ChangeHealth(damage * -1, this);
                    OnDoDamage?.Invoke(this, new VictimArgs(interactiveObject, damage, newHealth <= 0));
                    OnCollision -= InteractiveObject_OnCollision;
                }
            }
            CallOnDestroyEvent();
        }

        public override Entity Clone()
        {
            return new Projectile(Size, animations.Clone(), IsMoveAble, Speed);
        }
    }
}
