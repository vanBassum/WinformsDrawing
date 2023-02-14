using Draw.Elements;

namespace Draw.Manipulators
{

    /// <summary>
    /// Base class for all Manipulator implementations.
    /// </summary>
    public abstract class Manipulator : IManipulator
    {
        /// <summary>
        /// Called to register event callbacks on the target element.
        /// </summary>
        protected abstract void RegisterCallbacksOnTarget();
        /// <summary>
        /// Called to unregister event callbacks from the target element.
        /// </summary>
        protected abstract void UnregisterCallbacksFromTarget();

        VisualElement m_Target;
        /// <summary>
        /// VisualElement being manipulated.
        /// </summary>
        public VisualElement Target
        {
            get { return m_Target; }
            set
            {
                if (Target != null)
                {
                    UnregisterCallbacksFromTarget();
                }
                m_Target = value;
                if (Target != null)
                {
                    RegisterCallbacksOnTarget();
                }
            }
        }
    }
}