/* 
 * Purpose: Gives Attributes and Important Methods to Characters
 * Author: Fabian Subat
 * Date: 04.11.2016
 */

 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveryonesHell.EntityManagment
{
    public class Character : InteractiveObjects
    {
        AnimationManager animations;
        private int currentHealth;
        private int maxHealth;

        /// <summary>
        /// Returns the current Health while prohibiting it to be changed from outside.
        /// </summary>
        public int CurrentHealth
        {
            get
            {
                return currentHealth;
            }
        }

        public Character()
        {

        }

        /// <summary>
        /// Can be called when the character suffers damage / gets hit.
        /// </summary>
        /// <param name="damageGained"></param>
        public void Damaged (int damageGained)
        {
            if((currentHealth - damageGained) >= 0)
            {
                currentHealth -= damageGained;
            }
            else
            {
                return;//TODO add Death
            }
        }

        /// <summary>
        /// Can be Called when the character gets healed.
        /// </summary>
        /// <param name="HealAmount"></param>
        public void Healed (int HealAmount)
        {
            if((currentHealth + HealAmount) <= maxHealth)
            {
                currentHealth += HealAmount;
            }
            else
            {
                currentHealth = maxHealth;
            }
        }

        public override void Update(float elapsedMilliseconds)
        {
            throw new NotImplementedException();
        }

        public override void Draw()
        {
            throw new NotImplementedException();
        }
    }
}
