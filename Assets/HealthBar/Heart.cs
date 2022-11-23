using System;
using UnityEngine;
using UnityEngine.UI;

namespace HealthBar
{
    public class Heart : MonoBehaviour
    {
        private Sprite[] _sprites;
        private Image _image;

        void Start()
        {
            _sprites = Resources.LoadAll<Sprite>("heart");
            _image = GetComponent<Image>();
        }

        public void SetPercentage(int percentage)
        {
            _image.sprite = _sprites[CalculateIndexFromPercentage(percentage, _sprites.Length)];
        }

        public static int CalculateIndexFromPercentage(int percentage, int numOfStates)
        {
            if (percentage == 100)
            {
                return numOfStates - 1;
            }

            if (percentage == 0)
            {
                return 0;
            }

            var index = (int)Math.Floor(percentage / 100f * (numOfStates - 2));
            return index <= 0 ? 1 : index;
        }
    }
}