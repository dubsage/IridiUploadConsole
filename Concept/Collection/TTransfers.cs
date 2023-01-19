using System.Collections.Generic;
using System.Linq;

namespace IridiUploadConsole.Concept.Collection
{
    class TTransfers
    {
        public List<Transfer> Transfers = new List<Transfer>();
        public int Current = -1;

        public int Count()
        {
            return Transfers.Count;
        }

        public void Add()
        {
            Transfer transfer = new Transfer();
            Transfers.Add(transfer);
            Current++;
        }

        public bool Delete(int index)
        {
            if (index < Count())
            {
                Current--;
                Transfers.RemoveAt(index);
                return true;
            }
            return false;
        }

        public bool Delete()
        {
            if (0 < Count())
            {
                Current--;
                Transfers.RemoveAt(Count() - 1);
                return true;
            }
            return false;
        }

        public bool SetCurrent(int index)
        {
            if (index > -1 && index < Count())
            {
                Current = index;
                return true;
            }
            return false;
        }

        public Transfer Get(int index)
        {
            if (index < Count())
                return Transfers.ElementAt(index);

            return default;
        }

        public Transfer Get()
        {
            if (Current > -1 && Current < Count())
                return Transfers.ElementAt(Current);

            return default;
        }

    }
}
