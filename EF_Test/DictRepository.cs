using System;
using System.Collections.Generic;
using System.Text;

namespace EF_Test
{
    public partial class DictRepository:BaseRepository<Dict>
    {
        public DictRepository(BaseDbContext dbContext) : base(dbContext)
        {

        }
    }
}
