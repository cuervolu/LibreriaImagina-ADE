using SistemaLibreriaImagina.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
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

        public static async Task<List<USUARIO>> GetRepartidores()
        {
            try
            {
                using (Entities dbContext = new Entities())
                {
                    List<USUARIO> repartidores = await dbContext.USUARIOs
                        .Where(u => u.TIPO_USUARIO == "Repartidor")
                        .ToListAsync();

                    return repartidores;
                }
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                return null;
            }
        }

        public static bool UpdateAdminStatus(long id, bool isAdmin)
        {
            try
            {
                using (Entities dbContext = new Entities())
                {
                    USUARIO user = dbContext.USUARIOs.FirstOrDefault(u => u.ID == id);
                    if (user.IS_SUPERUSER == isAdmin) return false;
                    if (user != null)
                    {
                        user.IS_SUPERUSER = isAdmin;
                        dbContext.SaveChanges();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al actualizar el usuario con ID {id}: {ex.Message}");
            }
        }


        public static bool UpdateActiveStatus(long id, bool isActive)
        {
            try
            {
                using (Entities dbContext = new Entities())
                {
                    USUARIO user = dbContext.USUARIOs.FirstOrDefault(u => u.ID == id);
                    if (user.IS_ACTIVE == isActive) return false;

                    if (user != null)
                    {
                        user.IS_ACTIVE = isActive;
                        dbContext.SaveChanges();
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al actualizar el usuario con ID {id}: {ex.Message}");
            }
        }

        public static bool UpdateStaffStatus(long id, bool isStaff)
        {
            try
            {
                using (Entities dbContext = new Entities())
                {
                    USUARIO user = dbContext.USUARIOs.FirstOrDefault(u => u.ID == id);
                    if (user.IS_STAFF == isStaff) return false;
                    if (user != null)
                    {
                        user.IS_STAFF = isStaff;

                        if (!user.IS_SUPERUSER && !isStaff)
                        {
                            user.TIPO_USUARIO = "Cliente";
                        }
                        else if (user.TIPO_USUARIO == "Cliente" && isStaff)
                        {
                            user.TIPO_USUARIO = "Empleado";
                        }

                        dbContext.SaveChanges(); // Guardar los cambios en la base de datos
                        return true; // Actualización exitosa
                    }
                    else
                    {
                        return false; // Usuario no encontrado
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al actualizar el usuario con ID {id}: {ex.Message}");
            }
        }



    }
}
