
using UnityEngine.EventSystems;
namespace BallistaShooter
{
    public class NullBuildSite : BuildSite
    {
        public override void OnPointerDown(PointerEventData eventData)
        {
            HideControls();
        }
    }
}
