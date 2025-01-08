using People.Models;
using SQLite;

namespace People;

public class PersonRepository
{
    private SQLiteConnection conn;
    private void Init()
    {
        if (conn != null)
            return;

        conn = new SQLiteConnection(_dbPath);
        conn.CreateTable<EGrandaPerson>();
    }

    string _dbPath;

    public string StatusMessage { get; set; }

    public PersonRepository(string dbPath)
    {
        _dbPath = dbPath;                        
    }

    public void AddNewPerson(string name)
    {            
        int result = 0;
        try
        {
            // TODO: Call Init()
            Init();
            // basic validation to ensure a name was entered
            if (string.IsNullOrEmpty(name))
                throw new Exception("Valid name required");

            // TODO: Insert the new person into the database
            result = conn.Insert(new EGrandaPerson { Name = name }); 

            StatusMessage = string.Format("{0} record(s) added (Name: {1})", result, name);
        }
        catch (Exception ex)
        {
            StatusMessage = string.Format("Failed to add {0}. Error: {1}", name, ex.Message);
        }

    }
    public void DeletePerson(EGrandaPerson person)
    {
        try
        {
            
            int result = conn.Delete(person);

            StatusMessage = result > 0 ? "Registro eliminado con éxito." : "No se encontró el registro para eliminar.";
        }
        catch (Exception ex)
        {
            StatusMessage = string.Format("Failed to delete {0}. Error: {1}", person.Name, ex.Message);
        }
    }

    public List<EGrandaPerson> GetAllPeople()
    {
        try
        {
            Init();
            return conn.Table<EGrandaPerson>().ToList();
        }
        catch (Exception ex)
        {
            StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
        }

        return new List<EGrandaPerson>();
    }
}
