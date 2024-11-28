﻿using KooliProjekt.Data;
using KooliProjekt.Search;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Services
{
    public class RentingService : IRentingService
    {
        private readonly ApplicationDbContext _context;

        public RentingService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Delete(int id)
        {
            await _context.Rentings
                .Where(list => list.Id == id)
                .ExecuteDeleteAsync();
        }

        public async Task<Renting> Get(int id)
        {
            return await _context.Rentings.FindAsync(id);
        }

        public async Task<PagedResult<Renting>> List(int page, int pageSize, CarsSearch search = null)
        {
            var query = _context.Rentings.AsQueryable();

            search = search ?? new RentingsSearch();

            if (!string.IsNullOrWhiteSpace(search.Keyword))
            {
                query = query.Where(list => list.RentalNo.Contains(search.Keyword));
            }

            if (search.Done != null)
            {
                query = query.Where(list => list.M.Any());

                if (search.Done.Value)
                {
                    query = query.Where(list => list.Lines.All(item => item.IsDone));
                }
                else
                {
                    query = query.Where(list => list.Lines.Any(item => !item.IsDone));
                }
            }

            return await query
                .OrderBy(list => list.RentalNo
                .GetPagedAsync(page, pageSize);
        }

        public async Task Save(Renting list)
        {
            if (list.Id == 0)
            {
                _context.Rentings.Add(list);
            }
            else
            {
                _context.Rentings.Update(list);
            }

            await _context.SaveChangesAsync();
        }
    }
}
}
