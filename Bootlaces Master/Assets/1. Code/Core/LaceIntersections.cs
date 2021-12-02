﻿using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace BootlacesMaster
{
    public class LaceIntersections : MonoBehaviour
    {
        [SerializeField] private Color _intersectionColor = Color.red;
        [SerializeField] private Color _intersectionPointColor = Color.green;
        [SerializeField] private float _intersectionPointRadius = 0.5f;

        private Lace[] _laces = null;

        private Dictionary<ILace, Color> _colors;

        private void Awake()
        {
            _laces = FindObjectsOfType<Lace>().ToArray();

            _colors = new Dictionary<ILace, Color>();

            foreach (var lace in _laces)
            {
                _colors.Add(lace, Random.ColorHSV(0f, 1f, 0.7f, 0.7f, 0.8f, 0.8f));
            }
        }

        private void OnDrawGizmos()
        {
            if (_laces == null)
                return;

            foreach (var lace in _laces)
            {
                Gizmos.color = _colors[lace];

                foreach (var otherLace in _laces.Except(lace.Yield()))
                {
                    if (lace.Intersects(otherLace, out var intersection))
                    {
                        Gizmos.color = _intersectionPointColor;
                        Gizmos.DrawSphere(intersection, _intersectionPointRadius);
                        Gizmos.color = _intersectionColor;
                    }
                }

                var positions = lace.Points.ToList();

                for (int i = 1; i < positions.Count; ++i)
                {
                    Gizmos.DrawLine(positions[i - 1], positions[i]);
                }
            }
        }
    }
}