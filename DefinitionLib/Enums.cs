using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefinitionLib
{
    public enum EventType
    {
        Purchase = 1, Destruction = 2, Borrowing = 3, Return = 4
    }

    public enum CopyCondition
    {
        Mint = 6, NearMint = 5, Good = 4, Poor = 3, Damaged = 2, HeavlyDamaged = 1
    }

    public enum LiteraryGenre
    {
        SciFi = 1, Fatansy = 2, Comedy = 3, Horror = 4, Thriller =5, Romance = 6, 
    }
}
