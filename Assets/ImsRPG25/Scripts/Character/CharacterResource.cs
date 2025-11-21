using System;
using UnityEngine;
using UnityEngine.UI;

namespace ImsGDK    // 'Immersive Studios' -> 'Game Development Kit (GDK)'.
{
    public class CharacterResource : MonoBehaviour
    {
        // An 'amount' value for the character with minimum & maximum limits.

        // - Attach this script as a GameObject Component.
        //   - Like '/Player/Inventory/HealthPotion/' has 'Character Resource (Script)'.
        //   - The 'HealthPotion' GameObject name then becomes the unique name
        //     to find other UI GameObjects in Scene GameObject hierarchy.
        // - Designer can configure a 'HealthPotion' -> 'amount' in Unity IDE.
        // - Designer can configure a 'HealthPotion' -> 'amountMinimum' in Unity IDE.
        // - Designer can configure a 'HealthPotion' -> 'amountMaximum' in Unity IDE.

        [Header("Character Amount")]

        [SerializeField] public float amount = 0;
        [SerializeField] public float amountMinimum = 0;
        [SerializeField] public float amountMaximum = 9999;
        [SerializeField] public string uiHudGO_Prefix = "Player";
        [SerializeField] public string uiFormatAmount = "F1";
        [SerializeField] public string uiFormatAmountMinimum = "F0";
        [SerializeField] public string uiFormatAmountMaximum = "F0";

        // Character UI gameobjects representing the above amounts.
        private UnityEngine.UI.Text _canvasPlayHUDCharacterAmountText;
        private UnityEngine.UI.Text _canvasPlayHUDCharacterAmountMinimumText;
        private UnityEngine.UI.Text _canvasPlayHUDCharacterAmountMaximumText;
        private UnityEngine.UI.Image _canvasPlayHUDCharacterAmountBar;

        // Flag to minimise redundant UI processing ... due to OOP Inheritance complications.
        protected bool __doShowNow = false;

        // Start is called once before the first execution of Update after the MonoBehaviour is created.
        virtual public void Start()
        {
            DoValidate();
            DoOnce();
            __doShowNow = true;
        }

        // Update is called once per frame.
        virtual public void Update()
        {
            if (__doShowNow)
            {
                DoShow();
            }
        }

        virtual public void DoValidate()
        {
            // Ensure amountMinimum & amountMaximum are sequential.
            if (amountMinimum > amountMaximum)
            {
                #pragma warning disable IDE0180
                float minimum = amountMinimum;
                amountMinimum = amountMaximum;
                amountMaximum = minimum;
            }

            // Ensure amount does not exceed amountMinimum.
            if (amount < amountMinimum)
            {
                amount = amountMinimum;
            }

            // Ensure amount does not exceed amountMaximum.
            if (amount > amountMaximum)
            {
                amount = amountMaximum;
            }
        }

        virtual public void DoOnce()
        {
            // Find Character amount text in HUD ... only once.
            if (null == _canvasPlayHUDCharacterAmountText)
            {
                GameObject amountGO = GameObject.Find($"{uiHudGO_Prefix}{name}AmountText");
                if (null != amountGO)
                {
                    _canvasPlayHUDCharacterAmountText = amountGO.GetComponent<UnityEngine.UI.Text>() as UnityEngine.UI.Text;
                }
            }

            // Find Character amount text minimum in HUD ... only once.
            if (null == _canvasPlayHUDCharacterAmountMinimumText)
            {
                GameObject amountGO = GameObject.Find($"{uiHudGO_Prefix}{name}AmountMinimumText");
                if (null != amountGO)
                {
                    _canvasPlayHUDCharacterAmountMinimumText = amountGO.GetComponent<UnityEngine.UI.Text>() as UnityEngine.UI.Text;
                }
            }

            // Find Character amount text maximum in HUD ... only once.
            if (null == _canvasPlayHUDCharacterAmountMaximumText)
            {
                GameObject amountGO = GameObject.Find($"{uiHudGO_Prefix}{name}AmountMaximumText");
                if (null != amountGO)
                {
                    _canvasPlayHUDCharacterAmountMaximumText = amountGO.GetComponent<UnityEngine.UI.Text>() as UnityEngine.UI.Text;
                }
            }

            // Find Character amount image bar in HUD ... only once.
            if (null == _canvasPlayHUDCharacterAmountBar)
            {
                GameObject amountGO = GameObject.Find($"{uiHudGO_Prefix}{name}AmountBar");
                if (null != amountGO)
                {
                    _canvasPlayHUDCharacterAmountBar = amountGO.GetComponent<UnityEngine.UI.Image>() as UnityEngine.UI.Image;
                }
            }
        }

        virtual public void DoShow()
        {
            DoShowAmountText();         // Like amount.ToString("F2").
            DoShowAmountMinimumText();  // Like amountMinimum.ToString("F0").
            DoShowAmountMaximumText();  // Like amountMaximum.ToString("F0").
            DoShowAmountBar();          // Like (amount / amountMaximum).
        }

        virtual protected void DoShowAmountText()
        {
            if (null == _canvasPlayHUDCharacterAmountText)
            {
                return;
            }

            // Show the amount value as UI Text string.
            _canvasPlayHUDCharacterAmountText.text = amount.ToString(uiFormatAmount);
        }

        virtual protected void DoShowAmountMinimumText()
        {
            if (null == _canvasPlayHUDCharacterAmountMinimumText)
            {
                return;
            }

            // Show the amount minimum value as UI Text string.
            _canvasPlayHUDCharacterAmountMinimumText.text = amountMinimum.ToString(uiFormatAmountMinimum);
        }

        virtual protected void DoShowAmountMaximumText()
        {
            if (null == _canvasPlayHUDCharacterAmountMaximumText)
            {
                return;
            }

            // Show the amount maximum value as UI Text string.
            _canvasPlayHUDCharacterAmountMaximumText.text = amountMaximum.ToString(uiFormatAmountMaximum);
        }

        virtual protected void DoShowAmountBar()
        {
            if (null == _canvasPlayHUDCharacterAmountBar)
            {
                return;
            }

            // Show the percentage value as a UI Image bar.
            _canvasPlayHUDCharacterAmountBar.fillAmount = Mathf.Clamp01(amount / amountMaximum);
        }
    }
}
