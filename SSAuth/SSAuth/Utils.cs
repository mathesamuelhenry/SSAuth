using Microsoft.EntityFrameworkCore;
using SSAuth.Data.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSAuth
{
    public static class Utils
    {
        public async static Task<int> GetNextIdAsync(SSAuthContext dbContext, string table_name)
        {
            var tableNextIDObject = await dbContext.SeqControl.Where(x => x.ObjName == table_name).FirstOrDefaultAsync();
            tableNextIDObject.NextId += 1;
            dbContext.Entry(tableNextIDObject).Property(x => x.NextId).IsModified = true;
            await dbContext.SaveChangesAsync();

            return tableNextIDObject.NextId;
        }
    }
}
