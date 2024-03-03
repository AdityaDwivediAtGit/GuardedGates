using DapperMVCLearning.Data.DataAccess;
using DapperMVCLearning.Data.Models.Domain;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperMVCLearning.Data.Repository
{
    public class PersonRepository : IPersonRepository
    {
        public readonly ISqlDataAccess _db;
        public PersonRepository(ISqlDataAccess db) { _db = db; }

        // Creater 
        public async Task<bool> AddAsync(Person person)
        {
            try {
                await _db.SaveData("sp_create_person", new { person.Name, person.Email, person.Address });
                return true;
            }
            catch (Exception ex) { return false; }
        }

        // Updater
        public async Task<bool> UpdateAsync(Person person)
        {
            try
            {
                //await _db.SaveData("sp_update_person", new {person.Id, person.Name, person.Email, person.Address });
                // above line is same as below line
                await _db.SaveData("sp_update_person", person);
                return true;
            }
            catch (Exception ex) { return false; }
        }

        // Deleter
        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                await _db.SaveData("sp_delete_person", new { Id = id });
                return true;
            }
            catch (Exception ex) { return false; }
        }

        // Single person Getter
        public async Task<Person?> GetByIdAsync(int id)
        {
            IEnumerable<Person> result = await _db.GetData<Person, dynamic>("sp_get_person", new { Id = id });
            return result.FirstOrDefault();
        }

        // All People Getter
        public async Task<IEnumerable<Person>> GetAllAsync()
        {
            IEnumerable<Person> people = await _db.GetData<Person, dynamic>("sp_get_people", new { });
            return people;
        }
    }
}
