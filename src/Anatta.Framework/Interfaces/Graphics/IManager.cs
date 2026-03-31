using Anatta.Framework.Interfaces.Graphics;
using OpenTK.Mathematics;

namespace Anatta.Framework.Interfaces.Graphics;

public interface IManager {
    static Vector2i ScreenSize { get; set; }
    
    void Add(IManageable managed);
    void AddRange(params IManageable[] managedItems);
    void Remove(IManageable managed);
    
    IEnumerable<IManageable> GetAll();
    
    void Update();
    void Draw();
}