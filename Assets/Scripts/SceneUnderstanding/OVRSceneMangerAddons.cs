using System.Collections; 
using System.Linq; 
using Unity.VisualScripting; 
using UnityEngine;

namespace MetaAdvancedFeatures.SceneUnderstanding
{
    public class OVRSceneMangerAddons : MonoBehaviour
    {
        protected OVRSceneManager SceneManager { get; private set; }

        private void Awake()
        {
            SceneManager = GetComponent<OVRSceneManager>();
        }

        void Start()
        {
            SceneManager.SceneModelLoadedSuccessfully += OnSceneModelLoadedSuccessfully;
        }

        private void OnSceneModelLoadedSuccessfully()
        {
            StartCoroutine(AddCollidersAndFixClassifications());
        }

        // [Note] jackyangzzh: to avoid racing condition, wait for end of frame for all prefabs to be populated properly before continuing
        private IEnumerator AddCollidersAndFixClassifications()
        {
            yield return new WaitForEndOfFrame();

            OVRSemanticClassification[] allClassifications = FindObjectsOfType<OVRSemanticClassification>()
                .Where(x => x.Contains(OVRSceneManager.Classification.Desk) ||
                            x.Contains(OVRSceneManager.Classification.Table)).ToArray();

            foreach (var classification in allClassifications)
            {
                //For some reason the desk model is upside down, so we need to flip it
                classification.transform.localScale = new Vector3(classification.transform.localScale.x, classification.transform.localScale.y,
                    classification.transform.localScale.z * -1);
                ChangePositionRotationDesk(classification.transform);
            }
        }

        private void ChangePositionRotationDesk(Transform transform)
        {
            Transform[] allChildren = transform.GetComponentsInChildren<Transform>(true);
            //loop through the transform of the object until finding the desk
            foreach (Transform child in allChildren)
            {
                if (child.name == "DeskPrefab")
                {
                    child.localPosition = new Vector3(0.444000006f, 0.648999989f,
                        -0.345999986f);
                    child.localRotation = Quaternion.Euler(child.rotation.x, 238.852173f, 0);
                }
            }
        }
    }
}