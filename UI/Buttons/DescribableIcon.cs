using AnyRPG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace AnyRPG {
    public class DescribableIcon : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

        private IDescribable describable = null;

        [SerializeField]
        protected Text stackSize;

        protected int count;

        public Image MyIcon {
            get {
                return icon;
            }

            set {
                icon = value;
            }
        }

        public virtual int MyCount {
            get {
                return count;
            }
        }

        public Text MyStackSizeText {
            get {
                return stackSize;
            }
        }

        public IDescribable MyDescribable { get => describable; set => describable = value; }

        [SerializeField]
        protected Image icon;

        protected virtual void Awake() {
            //Debug.Log("ActionButton.Awake()");
            //MyDescribable = null;
        }

        protected virtual void Start() {
            //Debug.Log("ActionButton.Start()");
        }


        /// <summary>
        /// Sets the describable on the describablebutton
        /// </summary>
        /// <param name="describable"></param>
        public virtual void SetDescribable(IDescribable describable) {
            //Debug.Log("DescribableIcon.SetDescribable(" + (describable == null ? "null" : describable.MyName) + ")");
            SetDescribableCommon(describable);
        }

        public virtual void SetDescribable(IDescribable describable, int count) {
            //Debug.Log("DescribableIcon.SetDescribable(" + describable.MyName + ")");
            this.count = count;
            SetDescribableCommon(describable);
        }

        protected virtual void SetDescribableCommon(IDescribable describable) {
            //Debug.Log("DescribableIcon.SetDescribableCommon(" + (describable == null ? "null" : describable.MyName) + ")");
            this.MyDescribable = describable;
            UpdateVisual();
            
            //Debug.Log("Mouse Position: " + Input.mousePosition);
            //Debug.Log("RectTransformToScreenSpace: " + RectTransformToScreenSpace(MyIcon.rectTransform));
            //Debug.Log("Rect Contains Mouse: " + RectTransformUtility.RectangleContainsScreenPoint(MyIcon.rectTransform, Input.mousePosition));
            //Debug.Log("New MouseInRect: " + MouseInRect(MyIcon.rectTransform));

            if (UIManager.MouseInRect(MyIcon.rectTransform)) {
                //if (RectTransformUtility.RectangleContainsScreenPoint(MyIcon.rectTransform, Input.mousePosition)) {
                //UIManager.MyInstance.RefreshTooltip(describable as IDescribable);
                UIManager.MyInstance.ShowToolTip(transform.position, describable as IDescribable);
            }

        }

        

        public static Rect RectTransformToScreenSpace(RectTransform transform) {
            Vector2 size = Vector2.Scale(transform.rect.size, transform.lossyScale);
            float x = transform.position.x + transform.anchoredPosition.x;
            float y = Screen.height - transform.position.y - transform.anchoredPosition.y;

            return new Rect(x, y, size.x, size.y);
        }

        public virtual void UpdateVisual(Item item) {
            //Debug.Log("DescribableIcon.UpdateVisual()");
            /*
            if ((item as IDescribable) == MyDescribable) {
                count = InventoryManager.MyInstance.GetItemCount(item.MyName);
            }
            */
            UpdateVisual();
        }

        /// <summary>
        /// UPdates the visual representation of the describablebutton
        /// </summary>
        public virtual void UpdateVisual() {
            //Debug.Log("DescribableIcon.UpdateVisual()");
            if (MyDescribable == null) {
                //Debug.Log("DescribableIcon.UpdateVisual(): MyDescribable is null!");
            }
            if (MyIcon == null) {
                //Debug.Log("DescribableIcon.UpdateVisual(): MyIcon is null!");
            }
            if (MyDescribable != null && MyIcon != null) {
                if (MyIcon.sprite != MyDescribable.MyIcon) {
                    //Debug.Log("DescribableIcon.UpdateVisual(): Updating Icon for : " + MyDescribable.MyName);
                    MyIcon.sprite = null;
                    MyIcon.sprite = MyDescribable.MyIcon;
                    MyIcon.color = Color.white;
                }
            } else if (MyDescribable == null && MyIcon != null) {
                MyIcon.sprite = null;
                MyIcon.color = new Color32(0, 0, 0, 0);
            }

            /*
            if (count > 1) {
                UIManager.MyInstance.UpdateStackSize(this, count);
            } else if (MyDescribable is BaseAbility) {
                UIManager.MyInstance.ClearStackCount(this);
            }
            */
        }

        public virtual void OnPointerEnter(PointerEventData eventData) {
            //Debug.Log("DescribableIcon.OnPointerEnter()");
            IDescribable tmp = null;

            if (MyDescribable != null && MyDescribable is IDescribable) {
                tmp = (IDescribable)MyDescribable;
                //Debug.Log("DescribableIcon.OnPointerEnter(): describable is not null");
                //UIManager.MyInstance.ShowToolTip(transform.position);
            } else {
                if (MyDescribable == null) {
                    //Debug.Log("DescribableIcon.OnPointerEnter(): describable is null");
                }
                if (!(MyDescribable is IDescribable)) {
                    //Debug.Log("DescribableIcon.OnPointerEnter(): describable is not IDescribable");
                }
            }
            if (tmp != null) {
                //Debug.Log("DescribableIcon.OnPointerEnter(): showing tooltip");
                ShowToolTip(tmp);
            }
        }

        public virtual void ShowToolTip(IDescribable describable) {
            UIManager.MyInstance.ShowToolTip(transform.position, describable);
        }

        public virtual void OnPointerExit(PointerEventData eventData) {
            UIManager.MyInstance.HideToolTip();
        }

        public virtual void OnDisable() {
        }
    }

}