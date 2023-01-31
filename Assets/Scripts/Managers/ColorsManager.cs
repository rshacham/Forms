using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Managers
{
    public class ColorsManager : MonoBehaviour
    {
        public GameObject ActivePlayer { get; set; }

        [Header("Colors of not moving platforms")]
        [SerializeField] private GameObject[] platforms;
        [SerializeField] private Color platformColorInCircle;
        [SerializeField] private Color platformColorInSquare;
        [SerializeField] private Color platformColorInTriangle;
    
        private Color colorOfPlatforms;
    
        [Header("Colors of moving platforms")]
        [SerializeField] private GameObject[] movingPlatforms;
        [SerializeField] private Color movingPlatformColorInCircle;
        [SerializeField] private Color movingPlatformColorInSquare;
        [SerializeField] private Color movingPlatformColorInTriangle;
    
        private Color colorOfMovingPlatforms;
    
        [Header("Colors for the player")]
        [SerializeField] private Color colorOfCircle;
        [SerializeField] private Color colorOfSquare;
        [SerializeField] private Color colorOfTriangle;
    
        private Color colorOfCurrentPlayer;

        public Color colorCircle
        {
            get => colorOfCircle;
        }
        
        public Color colorSquare
        {
            get => colorOfSquare;
        }
        
        public Color colorTriangle
        {
            get => colorOfTriangle;
        }

        private void Start()
        {
            colorOfCurrentPlayer = colorOfCircle;
            colorOfMovingPlatforms = movingPlatformColorInCircle;
            colorOfPlatforms = platformColorInCircle;
        }

        private void ChangeWorldColors()
        {
            // change color of current player
            ActivePlayer.GetComponent<SpriteRenderer>().color = colorOfCurrentPlayer;
        
            // change colors of not moving platforms
            foreach (var platform in platforms)
            {
                // platform.GetComponent<SpriteRenderer>().color = colorOfPlatforms;
                // for (int i = 0; i < platform.transform.childCount; i++)
                // {
                //     //platform.gameObject.transform.GetChild(i).GetComponent<Renderer>().material.color = colorOfPlatforms;
                //     platform.gameObject.transform.GetChild(i).GetComponent<SpriteRenderer>().color = colorOfPlatforms;
                // }

                var allRenderers = platform.GetComponentsInChildren<SpriteRenderer>();
                foreach (var sprite in allRenderers)
                {
                    if (sprite.color.a != 0)
                    {
                        sprite.color = colorOfPlatforms;
                    }
                }
            }
        
            // change colors of moving platforms
            foreach (var platform in movingPlatforms)
            {
                // platform.GetComponent<SpriteRenderer>().color = colorOfMovingPlatforms;
                // for (int i = 0; i < platform.transform.childCount; i++)
                // {
                //     platform.gameObject.transform.GetChild(i).GetComponent<SpriteRenderer>().color = colorOfMovingPlatforms;
                // }
                
                var allRenderers = platform.GetComponentsInChildren<SpriteRenderer>();
                foreach (var sprite in allRenderers)
                {
                    if (sprite.color.a != 0)
                    {
                        sprite.color = colorOfMovingPlatforms;
                    }
                }
            }
        }

        public void ChangeCircleColor()
        {
            colorOfCurrentPlayer = colorOfCircle;
            colorOfPlatforms = platformColorInCircle;
            colorOfMovingPlatforms = movingPlatformColorInCircle;
            ChangeWorldColors();
        }

        public void ChangeSquareColor()
        {
            colorOfCurrentPlayer = colorOfSquare;
            colorOfPlatforms = platformColorInSquare;
            colorOfMovingPlatforms = movingPlatformColorInSquare;
            ChangeWorldColors();
        }

        public void ChangeTriangleColor()
        {
            colorOfCurrentPlayer = colorOfTriangle;
            colorOfPlatforms = platformColorInTriangle;
            colorOfMovingPlatforms = movingPlatformColorInTriangle;
            ChangeWorldColors();
        }
    }
}