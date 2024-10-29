using LogifyBackEnd.Data;
using LogifyBackEnd.Services.Interfaces;

namespace LogifyBackEnd.Services;

public class DriverService : IDriverService
{
    private readonly DBContext _context;

    public DriverService(DBContext context)
    {
        _context = context;
    }

    // a. Accept request from employer
    public async Task<bool> AcceptRequest(int employerId, int driverId)
    {
        var driver = await _context.Drivers.FindAsync(driverId);

        if (driver == null || driver.Status == "Occupied")
            return false;

        driver.Status = "Accepted";
        await _context.SaveChangesAsync();
        return true;
    }
}