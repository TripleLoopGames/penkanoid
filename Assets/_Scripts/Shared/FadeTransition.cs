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

    public FadeTransition SetColor(Color color)
    {
        this.color = color;
        return this;
    }

    public IPromise Enter(float time = 1f)
    {
        return new Promise((resolve, reject) =>
        {
            Color colorToTransparent = new Color(this.color.r, this.color.g, this.color.b, 0);
            this.image.DOColor(colorToTransparent, time).OnComplete(() => resolve());
        });
    }

    public IPromise Exit(float time = 1f)
    {
        return new Promise((resolve, reject) =>
        {
            Color colorToSolid = new Color(this.color.r, this.color.g, this.color.b, 1);
            this.image.DOColor(colorToSolid, time).OnComplete(() => resolve());
        });
    }

    private Color color;
    private Image image;
}
