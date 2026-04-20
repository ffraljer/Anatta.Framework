namespace Anatta.Framework.Graphics.Interfaces;

public interface IManager {
    void Add(IManageable managed);
    void AddRange(params IManageable[] managedItems);
    void Remove(IManageable managed);
    
    IEnumerable<IManageable> GetAll();
    
    void Update();
    void Draw();
}