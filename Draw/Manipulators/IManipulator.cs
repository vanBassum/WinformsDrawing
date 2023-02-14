using Draw.Elements;

namespace Draw.Manipulators
{
    /// <summary>
    /// Interface for Manipulator objects.
    /// </summary>
    public interface IManipulator
    {
        /// <summary>
        /// VisualElement being manipulated.
        /// </summary>
        VisualElement Target { get; set; }
    }
}