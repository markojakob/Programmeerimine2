﻿using KooliProjekt.Data;
using KooliProjekt.Search;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ApplicationDbContext _context;

        public CustomerService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Delete(int id)
        {
            var customerToDelete = await _context.Customers.FindAsync(id);

            if (customerToDelete != null)
            {
                _context.Customers.Remove(customerToDelete);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Customer> Get(int id)
        {
            return await _context.Customers.FindAsync(id);
        }

        public async Task<PagedResult<Customer>> List(int page, int pageSize, CustomersSearch search = null)
        {
            var query = _context.Customers.AsQueryable();

            search = search ?? new CustomersSearch();

            if (!string.IsNullOrWhiteSpace(search.Keyword))
            {
                query = query.Where(list => list.FirstName.Contains(search.Keyword) || list.LastName.Contains(search.Keyword));
            }

            return await query
                .OrderBy(list => list.FirstName)
                .ThenBy(list => list.LastName)
                .GetPagedAsync(page, pageSize);
        }

        public async Task<IList<Customer>> Lookup()
        {
            return await _context.Customers.OrderBy(c => c.LastName).ThenBy(c => c.FirstName).ToListAsync();
        }

        public async Task Save(Customer list)
        {
            if (list.Id == 0)
            {
                _context.Add(list);
            }
            else
            {
                _context.Update(list);
            }

            await _context.SaveChangesAsync();
        }


    }
}