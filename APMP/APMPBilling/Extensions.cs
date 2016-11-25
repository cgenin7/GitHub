using System;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace APMPBilling
{
    public static class ExtensionMethods
    {
        public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek)
        {
            int diff = dt.DayOfWeek - startOfWeek;
            if (diff < 0)
            {
                diff += 7;
            }
            return dt.AddDays(-1 * diff).Date;
        }

        public static HtmlString NumericInputFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, int min, int max, object htmlAttributes)
        {
            XDocument _xml = XDocument.Parse(html.EditorFor(expression, htmlAttributes).ToString());
            XElement _element = _xml.Element("input");

            if (_element != null)
            {
                _element.SetAttributeValue("type", "number");
                _element.SetAttributeValue("min", min);
                _element.SetAttributeValue("max", max);
            }

            return new HtmlString(_xml.ToString());
        }

        public static HtmlString SpinnerFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression, object htmlAttributes)
        {
            XDocument _xml = XDocument.Parse(html.EditorFor(expression, htmlAttributes).ToString());
            XElement _element = _xml.Element("input");

            if (_element != null)
            {
                _element.SetAttributeValue("type", "number");

                if (_element.Attribute("data-val-range-max") != null)
                    _element.SetAttributeValue("max", _element.Attribute("data-val-range-max").Value);

                if (_element.Attribute("data-val-range-min") != null)
                    _element.SetAttributeValue("min", _element.Attribute("data-val-range-min").Value);
            }

            return new HtmlString(_xml.ToString());
        }

        public static IHtmlString TextAreaAutoSizeFor<TModel, TProperty>(
                this HtmlHelper<TModel> htmlHelper,
                Expression<Func<TModel, TProperty>> expression, int width_em, 
                object htmlAttributes=null )
            {
                var model = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData).Model;
                var text = model as string ?? string.Empty;
                int lines = 1;
                string[] arr = text.Split(new string[] { "\r\n", "\n", "\r" }, StringSplitOptions.None);
                foreach (var str in arr)
                {
                    if (str.Length / width_em > 0)
                    {
                        lines += str.Length / width_em + (str.Length % width_em <= width_em / 2 ? 1 : 0);
                    }
                    else
                    {
                        lines++;
                    }
                }
                var attributes = new RouteValueDictionary(htmlAttributes);
                attributes["style"] = string.Format("width:{0}em; height:{1}em;", width_em, lines);
                return htmlHelper.TextAreaFor(expression, attributes);
            }
        }
}