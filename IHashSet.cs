using System.Diagnostics;

IHashSet<ISPSStudent> mhs = new MyHashSet();
ISPSStudent m1 = new MyStudent("Alice", "2024", "Dr. Smith");
ISPSStudent m2 = new MyStudent("Bob", "2023", "Dr. Jones");
ISPSStudent m3 = new MyStudent("Charlie", "2024", "Dr. Smith");

mhs.Add(m1);
Debug.Assert(mhs.IsPresent(m1)); // Should be True
Debug.Assert(!mhs.IsPresent(m2)); // Should be False
mhs.Add(m2);    
mhs.Add(m3);
Debug.Assert(mhs.IsPresent(m2)); // Should be True
Console.WriteLine("All tests passed!");


// todo: implement chaining collision resolution strategy and add tests for it
// todo: implement resizing and rehashing when load factor is exceeded and add tests for it
// todo: create a test harness outside of this file to run all tests and report results in a more structured way

internal enum CollisionType 
{
    LinearProbing,
    Chaining
}

public class MyHashSet : IHashSet<ISPSStudent> 
{
    IList<ISPSStudent> _linearProbingSet;
    readonly float _loadFactor;
    readonly CollisionType _collisionType;

    public MyHashSet(int size = 10, CollisionType collisionType = CollisionType.LinearProbing, float loadFactor = 0.75f)
    {
        _linearProbingSet = new List<ISPSStudent>();
        _loadFactor = loadFactor;
        _collisionType = collisionType;
        for(int i = 0; i < size; ++i)
        {
            if(collisionType == CollisionType.LinearProbing)
            {
                _linearProbingSet.Add(null); // Initialize with default values
            }
            else
            {
                throw new NotImplementedException("Chaining collision resolution is not implemented yet.");
            }
        }
    }

    public ISPSStudent Add(ISPSStudent value)
    {
        if(_collisionType == CollisionType.LinearProbing)
        {
            return AddLinearProbing(value, _linearProbingSet);
        }
        else
        {
            throw new NotImplementedException("Chaining collision resolution is not implemented yet.");
        }
    }

    private ISPSStudent AddLinearProbing(ISPSStudent value, List<ISPSStudent> set)
    {
        if(value == null)
        {
            throw new ArgumentNullException(nameof(value));
        }
        int index = Math.Abs(value.GetHashCode()) % set.Count;
        while (set[index] != null)
        {
            if (set[index] == value)
            {
                return set[index]; // Value already exists
            }
            index = (index + 1) % set.Count; // Linear probing
        }
        set[index] = value; // Add new value
        return value;
    }

    public bool IsPresent(ISPSStudent value)
    {
        int index = Math.Abs(value.GetHashCode()) % _linearProbingSet.Count;
        while (_linearProbingSet[index] != null)
        {
            if (_linearProbingSet[index] == value)
            {
                return true; // Value is present
            }
            index = (index + 1) % _linearProbingSet.Count; // Linear probing
        }
        return false; // Value is not present
    }

    public void Rebalance()
    {
        int oldSize = _linearProbingSet.Count;
        int newSize = oldSize * 2;
        List<ISPSStudent> newSet = new List<ISPSStudent>(newSize);
        for (int i = 0; i < newSize; ++i)   
        {
            newSet.Add(null);
        }
        for(int i = 0; i < oldSize; ++i)
        {
            if (_linearProbingSet[i] != null)
            {
                Add(_linearProbingSet[i], newSet);
            }
        }        
        _linearProbingSet = newSet;
    }
}
public class MyStudent : ISPSStudent
{
    string _name, _year, _tutor;
    public string Name => _name;
    public string Year => _year;
    public string Tutor => _tutor;

    public MyStudent(string name, string year, string tutor)
    {
        _name = name;
        _year = year;
        _tutor = tutor;
    }

    public bool Equals(ISPSStudent? other)
    {
        if (other == null) return false;
        return Name == other.Name && Year == other.Year && Tutor == other.Tutor;
    }

    public override int GetHashCode()
    {
        return Name.GetHashCode() ^ Year.GetHashCode() ^ Tutor.GetHashCode();
    }

    public override string ToString()
    {
        return $"{Name} ({Year}, {Tutor})";
    }
}

// included from IHashSet.cs
public interface IHashSet<T> where T : ISPSStudent, IEquatable<T>
{
    T Add(T value);
    bool IsPresent(T value);
    void Rebalance();
}

// renamed from ISPSStudent to make clear that it is an interface 
public interface ISPSStudent : IEquatable<ISPSStudent>
{
    string Name { get; }
    string Year { get; }
    string Tutor { get; }
}
