using SistemaLibreriaImagina.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace SistemaLibreriaImagina.Services
{
    public class UsersService
    {
        public static async Task<List<USUARIO>> GetUsersList()
        {
            try
            {
                using (Entities dbContext = new Entities())
                {
                    return await dbContext.USUARIOs.ToListAsync();
                }
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                return null;
            }
        }

        public static async Task<USUARIO> GetCurrentUserInfo(long id)
        {
            try
            {
                using (Entities dbContext = new Entities())
                {
                    USUARIO user = await dbContext.USUARIOs.FindAsync(id);

                    return user;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
