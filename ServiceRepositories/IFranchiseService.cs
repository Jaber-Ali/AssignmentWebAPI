using AssignmentWebAPI.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssignmentWebAPI.ServiceRepositories
{
    public interface IFranchiseService 
    {
        Task<IEnumerable<Character>> GetAllCharactersInFranchiseAsync(int id);
        Task<IEnumerable<Movie>> GetAllMoviesInFranchiseAsync(int id);
        public Task<Franchise> AddFranchiseAsync(Franchise franchise);
        public Task<IEnumerable<Franchise>> GetAllFranchisesAsync();
        public Task<Franchise> GetSpecificFranchiseAsync(int id);
        public Task UpdateFranchiseAsync(Franchise franchise);
        public Task DeleteFranchiseAsync(int id);
        public bool FranchiseExists(int id);
 
      
    }
}
