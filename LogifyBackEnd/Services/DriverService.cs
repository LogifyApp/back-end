using LogifyBackEnd.Data;
using LogifyBackEnd.Models;
using LogifyBackEnd.Models.Enums;
using LogifyBackEnd.Services.Interfaces;

namespace LogifyBackEnd.Services;

public class DriverService(DBContext context) : IDriverService
{
    public async Task<bool> AcceptRequest(int employerId, int driverId)
    {
        var driver = await context.Drivers.FindAsync(driverId);

        if (driver == null || driver.Status != DriverStatus.Pending)
            return false;

        driver.Status = DriverStatus.Ready;

        var historyEntry = new EmployerDriverHistory
        {
            EmployerUserId = employerId,
            DriverUserId = driverId,
            StartDate = DateTime.Now,
            EndDate = null
        };

        context.EmployerDriverHistories.Add(historyEntry);

        await context.SaveChangesAsync();
        return true;
    }
}