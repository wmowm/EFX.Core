using System;
using System.Collections.Generic;
using System.Text;

namespace EF_Test
{
    public partial class ManagerRepository:BaseRepository<Manager>
    {
        public ManagerRepository(BaseDbContext dbContext) : base(dbContext)
        {

        }
    }
}
