using Cycloid.Models;
using Cycloid.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cycloid.Managers
{
    public class ProgramsManager : IProgramsManager
    {
        private readonly IProgramsService programsService;

        public ProgramsManager(IProgramsService programsService)
        {
            this.programsService = programsService;
        }

        public List<Program> GetAllPrograms()
        {
            var programGet = this.programsService.GetProgramsMethod();

            return programGet;
        }

        public IEnumerable<Program> GetProgramsPaging(string channelid, int skip, int take)
        {
            var programsListGot = this.GetAllPrograms();

            var getProgramsList = new List<Program>();

            //Teste de Ids
            //var firstChannelId = programsListGot[0].ChannelId.ToString();

            foreach (var p in programsListGot)
            {
                if (p.ChannelId == channelid)
                {
                    getProgramsList.Add(p);
                }
            }

            var ListToSend = getProgramsList.Skip(skip).Take(take);

            return ListToSend;
        }
    }
}
