using FundRaiser.Common.Database;
using FundRaiser.Common.Interfaces;
using FundRaiser.Common.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundRaiser.Common.Services
{
    public class MediaService: IMediaService
    {
        private readonly AppDbContext _context;

        public MediaService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Media> Create(Media media)
        {
            await _context.Media.AddAsync(media);
            await _context.SaveChangesAsync();

            return media;
        }

        public async Task<bool> Delete(int mediaId)
        {
            var media = await _context.Media.FirstOrDefaultAsync(m => m.Id == mediaId);

            if (media == null)
            {
                return false;
            }

            _context.Media.Remove(media);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<Media>> GetMedia(int mediaId)
        {
            return await _context.Media
            .Where(m => m.Id == mediaId)
            .ToListAsync();
        }

        public async Task<Media> Update(int mediaId, Media med)
        {
            var media = await _context.Media.FirstOrDefaultAsync(m => m.Id == mediaId);

            media.Description = med.Description ?? media.Description;
            media.Path = med.Path ?? media.Path;
            media.MediaType = med.MediaType;

            await _context.SaveChangesAsync();

            return media;
        }
    }
}
