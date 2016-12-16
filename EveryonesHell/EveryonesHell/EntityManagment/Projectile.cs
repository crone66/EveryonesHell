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
            OnCollision += InteractiveObject_OnCollision;
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
                if(otherObject is InteractiveObject)
                {
                    InteractiveObject interactiveObject = otherObject as InteractiveObject;
                    int newHealth = interactiveObject.ChangeHealth(damage * -1, this);

                    OnDoDamage?.Invoke(this, new VictimArgs(interactiveObject, damage, newHealth <= 0));                                        
                }
                //Do something with other object... otherwise just destroy projectile
                //otherObject
            }
            else
            {
                //Tile Collision
                if (this is Projectile)
                {
                    //OnKill?.Invoke(this, null);
                    //isMoveAble = false;
                    IsCollidable = false;
                    Visable = false;
                }
            }
        }

        public override Entity Clone()
        {
            return new Projectile(Size, animations.Clone(), IsMoveAble, Speed);
        }
    }
}
