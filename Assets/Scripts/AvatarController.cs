using UnityEngine;

[System.Serializable]
public class MapTransforms
{
    public Transform vrTarget;
    public Transform ikTarget;

    public UnityEngine.Vector3 trackingPositionOffset;
    public UnityEngine.Vector3 trackingRotationOffset;

    public void VRMapping()
    {
        ikTarget.position = vrTarget.TransformPoint(trackingPositionOffset);
        ikTarget.rotation = vrTarget.rotation * Quaternion.Euler(trackingRotationOffset);
    }
}

public class AvatarController : MonoBehaviour
{
    [SerializeField] private MapTransforms head;
    [SerializeField] private MapTransforms leftHand;
    [SerializeField] private MapTransforms rightHand;

    [SerializeField] private float turnSmoothness;
    [SerializeField] private Transform ikHead;
    [SerializeField] private UnityEngine.Vector3 headBodyOffset;

    private void LateUpdate()
    {
        transform.position = ikHead.position + headBodyOffset;
        transform.forward = UnityEngine.Vector3.Lerp(transform.forward, UnityEngine.Vector3.ProjectOnPlane(ikHead.forward, UnityEngine.Vector3.up).normalized, Time.deltaTime * turnSmoothness);

        head.VRMapping();
        leftHand.VRMapping();
        rightHand.VRMapping();
    }
}
