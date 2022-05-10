using System.Collections.Generic;
using UnityEngine;

public abstract class Bar : MonoBehaviour
{
    [SerializeField] protected Player _player;
    [SerializeField] protected IconTemplate _template;
    [SerializeField] protected GameObject _container;

    protected List<IconTemplate> _templates = new List<IconTemplate>();

    public void OnValueChanged(int value)
    {
        if (_templates.Count < value)
        {
            int createHearts = value - _templates.Count;

            for (int i = 0; i < createHearts; i++)
            {
                CreateIcon();
            }
        }
        else if (_templates.Count > value && _templates.Count != 0)
        {
            int deleteHearts = _templates.Count - value;

            for (int i = 0; i < deleteHearts; i++)
            {
                DestroyIcon(_templates[_templates.Count - 1]);
            }
        }

        if (value <= 0) 
        {
            _container.SetActive(false);
        }
    }

    public void DestroyIcon(IconTemplate template)
    {
        _templates.Remove(template);
        template.DestroyIcon();
    }

    public void CreateIcon()
    {
        IconTemplate newTemplate = Instantiate(_template, _container.transform);
        _templates.Add(newTemplate.GetComponent<IconTemplate>());
    }
}