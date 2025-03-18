using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    /// <summary>
    /// Base class for UI buttons, providing automatic button component loading 
    /// and click event handling. Any custom button should inherit from this class.
    /// </summary>
    public class BaseButton : GameMonoBehaviour
    {

        [Header("Base Button")]
        [SerializeField] protected Button button;

        /// <summary>
        /// Called when the script starts. Initializes the button and adds click events.
        /// </summary>
        protected override void Start()
        {
            base.Start();
            this.AddOnClickEvent();
        }

        /// <summary>
        /// Loads required components, ensuring the button reference is assigned.
        /// </summary>
        protected override void LoadComponents()
        {
            base.LoadComponents();
            this.LoadButton();
        }

        /// <summary>
        /// Loads the Button component if not already assigned.
        /// </summary>
        protected virtual void LoadButton()
        {
            if (this.button != null) return;
            this.button = GetComponent<Button>();
            Debug.LogWarning(transform.name + ": LoadButton", gameObject);
        }

        /// <summary>
        /// Adds the OnClick event to the button. Removes any existing listeners to prevent duplication.
        /// </summary>
        protected virtual void AddOnClickEvent()
        {
            if (this.button == null) return;

            this.button.onClick.RemoveAllListeners(); // Xóa các listener cũ (để tránh xung đột)
            this.button.onClick.AddListener(this.OnClick);

            Debug.Log(transform.name + ": Sự kiện OnClick đã được thêm!", gameObject);
        }

        /// <summary>
        /// Method to be overridden by child classes to define custom button click behavior.
        /// </summary>
        protected virtual void OnClick()
        {
        }
    }
}
