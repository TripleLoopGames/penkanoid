using RSG;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FadeTransition : MonoBehaviour
{

    public FadeTransition Initialize(Color color, bool transparent)
    {
        this.color = color;
        this.image = this.GetComponent<Image>();
        int alpha = transparent ? 0 : 1;
        this.image.color = new Color(this.color.r, this.color.g, this.color.b, alpha);
        return this;
    }

    public IPromise Enter()
    {
        return new Promise((resolve, reject) =>
        {
            Color colorToTransparent = new Color(this.color.r, this.color.g, this.color.b, 0);
            this.image.DOColor(colorToTransparent, 1f).OnComplete(() => resolve());
        });
    }

    public IPromise Exit()
    {
        return new Promise((resolve, reject) =>
        {
            Color colorToSolid = new Color(this.color.r, this.color.g, this.color.b, 1);
            this.image.DOColor(colorToSolid, 1f).OnComplete(() => resolve());
        });
    }

    private Color color;
    private Image image;
}
