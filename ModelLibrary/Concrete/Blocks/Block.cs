using ModelLibrary.Abstract.Blocks;
using Newtonsoft.Json;
using System;

namespace ModelLibrary.Concrete.Blocks
{
    public class Block : ABlock
    {
        public event EventHandler OnBlockDestroyed;

        public Block()
        {

        }

        public Block(Block original)
        {
            ID = original.ID;
            Ethereal = original.Ethereal;
            Hardness = original.Hardness;
            MaximumHealth = original.MaximumHealth;
            CurrentHealth = original.CurrentHealth;
            Worth = original.Worth;
            Info = original.Info;
        }

        [JsonConstructor]
        public Block(short ID, bool Ethereal = false, float Hardness = 0, float Health = 0, float Worth = 0, BlockInfo Info = null)
        {
            this.ID = ID;
            this.Ethereal = Ethereal;
            this.Hardness = Hardness;
            MaximumHealth = Health;
            CurrentHealth = Health;
            this.Worth = Worth;
            this.Info = Info;
        }

        public void TakeDamage(float damage)
        {
            if (CurrentHealth == 0 || Ethereal)
            {
                return;
            }

            CurrentHealth -= CalculatePenetrationDamage(damage);

            if (CurrentHealth <= 0)
            {
                CurrentHealth = 0;

                OnBlockDestroyed?.Invoke(this, EventArgs.Empty);
            }
        }

        public float PercentDamaged()
        {
            return 1.0f - CurrentHealth / MaximumHealth;
        }

        public float CalculatePenetrationDamage(float drillDamage)
        {
            return (drillDamage - Hardness) <= 0 ? 0 : (drillDamage - Hardness);
        }

        // Etheral blocks do not block player ship from moving on over them.
        public bool BlockIsEthereal() => Ethereal;
    }
}