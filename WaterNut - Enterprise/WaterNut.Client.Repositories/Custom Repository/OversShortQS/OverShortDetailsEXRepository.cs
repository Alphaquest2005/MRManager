
using Core.Common.Client.Repositories;
using OversShortQS.Client.Entities;
using OversShortQS.Client.DTO;


using System.Threading.Tasks;
using OversShortQS.Client.Services;
using AsycudaDocumentItem = CoreEntities.Client.Entities.AsycudaDocumentItem;
using OverShortDetailsEX = OversShortQS.Client.Entities.OverShortDetailsEX;

namespace OversShortQS.Client.Repositories 
{
   
    public partial class OverShortDetailsEXRepository : BaseRepository<OverShortDetailsEXRepository>
    {
        public async Task MatchToCurrentItem(int currentDocumentItem, OverShortDetailsEX osd)
        {
            using (var t = new OverShortDetailsEXClient())
            {
                await t.MatchToCurrentItem(currentDocumentItem, osd.DTO).ConfigureAwait(false);
            }
        }

        public async Task RemoveOverShortDetail(int osd)
        {
            using (var t = new OverShortDetailsEXClient())
            {
                await t.RemoveOverShortDetail(osd).ConfigureAwait(false);
            }
        }
        
    }
}

