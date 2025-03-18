using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{

    /// <summary>
    /// A base class for MonoBehaviours in the game that provides common functionality 
    /// such as loading components, resetting values, and enabling/disabling behavior.
    /// The methods are designed to be overridden in subclasses for custom behavior.
    /// </summary>
    public class GameMonoBehaviour : MonoBehaviour
    {

        /// <summary>
        /// Called when the script instance is being loaded.
        /// It automatically loads the components by calling the LoadComponents method.
        /// </summary>
        protected virtual void Awake()
        {
            this.LoadComponents();
        }

        /// <summary>
        /// Called when the script instance is first enabled.
        /// This method can be overridden for custom behavior at the start of the game object lifecycle.
        /// </summary>
        protected virtual void Start()
        {
            //For override
        }

        /// <summary>
        /// Called when the script is reset (e.g., in the inspector).
        /// It loads components and resets values by calling respective methods.
        /// </summary>
        protected virtual void Reset()
        {
            this.LoadComponents();
            this.ResetValue();
        }

        /// <summary>
        /// Loads the components of the object. This is a placeholder method for subclasses to implement.
        /// </summary>
        protected virtual void LoadComponents()
        {
            //For override
        }

        /// <summary>
        /// Resets any values for the game object. This is a placeholder for subclass customization.
        /// </summary>
        protected virtual void ResetValue()
        {
            //For override
        }

        /// <summary>
        /// Called when the object is enabled.
        /// This method is available for overriding to implement behavior on enabling the script.
        /// </summary>
        protected virtual void OnEnable()
        {
            //For override
        }

        /// <summary>
        /// Called when the object is disabled.
        /// This method is available for overriding to implement behavior on disabling the script.
        /// </summary>
        protected virtual void OnDisable()
        {
            //For override
        }
    }
}
