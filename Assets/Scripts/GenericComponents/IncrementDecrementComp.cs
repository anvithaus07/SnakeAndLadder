using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace SnakeAndLadder
{
    public class IncrementDecrementComp : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI m_parameterName;
        [SerializeField] private TextMeshProUGUI m_parameterValueTxt;
        [SerializeField] private Button m_incrementBtn;
        [SerializeField] private Button m_decrementBtn;
        [SerializeField] private TextMeshProUGUI m_minMaxInfoTxt;

        Action<int> OnIncrementBtnClicked;
        Action<int> OnDecrementBtnClicked;


        int mMaxValue = 0;
        int mMinValue = 0;

        private void OnEnable()
        {
            m_incrementBtn.onClick.AddListener(OnIncrementBtnClick);
            m_decrementBtn.onClick.AddListener(OnDecrementBtnClick);
        }

        private void OnDisable()
        {
            m_incrementBtn.onClick.RemoveAllListeners();
            m_decrementBtn.onClick.RemoveAllListeners();
        }

        public void Initailize(string parameterName, int maxValue, int minValue, int defaultValue, Action<int> onIncrementBtnClick, Action<int> onDecrementBtnClicked)
        {
            m_parameterName.text = parameterName;

            CurrentParameterValue = defaultValue;

            mMinValue = minValue;
            mMaxValue = maxValue;

            OnIncrementBtnClicked = onIncrementBtnClick;
            OnDecrementBtnClicked = onDecrementBtnClicked;

            SetButtonInteractability();
            SetMinMaxInfo();
        }
        public void UpdateMaxAndMinValues(int minValue, int maxValue)
        {
            mMinValue = minValue;
            mMaxValue = maxValue;
            CurrentParameterValue = minValue;

            SetMinMaxInfo();
            SetButtonInteractability();
        }

        void SetMinMaxInfo()
        {
            string text = string.Format("Min : {0}  Max:{1}", mMinValue, mMaxValue);
            m_minMaxInfoTxt.text = text;
        }

        void OnIncrementBtnClick()
        {
            if (CurrentParameterValue < mMaxValue)
            {
                CurrentParameterValue++;
                OnIncrementBtnClicked?.Invoke(CurrentParameterValue);
            }
            SetButtonInteractability();
        }

        void OnDecrementBtnClick()
        {
            if (mMinValue < CurrentParameterValue)
            {
                CurrentParameterValue--;
                OnDecrementBtnClicked?.Invoke(CurrentParameterValue);
            }
            SetButtonInteractability();
        }

        void SetButtonInteractability()
        {
            m_decrementBtn.interactable = mMinValue < CurrentParameterValue;
            m_incrementBtn.interactable = CurrentParameterValue < mMaxValue;
        }



        private int _CurrentParameterValue;
        int CurrentParameterValue
        {
            get
            {
                return _CurrentParameterValue;
            }

            set
            {
                _CurrentParameterValue = value;
                m_parameterValueTxt.text = _CurrentParameterValue.ToString();
            }
        }
    }
}