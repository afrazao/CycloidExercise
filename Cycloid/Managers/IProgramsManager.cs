
using System.Collections.Generic;

namespace Cycloid.Managers
{
    public interface IProgramsManager
    {
        List<Models.Program> GetAllPrograms();
        IEnumerable<Models.Program> GetProgramsPaging(string channelid, int skip, int take);
    }
}
