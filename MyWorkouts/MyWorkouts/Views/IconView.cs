using System;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Util;
using Android.Views;

namespace Tasprof.App.MyWorkouts.Views
{
    public class IconView : View
    {
        protected Drawable _icon;
        protected string _labelText;
        protected bool _showIconLabel;

        protected readonly int iconLabelBottomMargin = (int)DimensionHelper.DpToPx(8);
        protected Color iconBackgroundColor, iconLabelTextColor;
        protected Paint iconBackgroundPaint, iconLabelTextColorPaint;
        protected float iconLabelTextSize = DimensionHelper.SpToPx(13);
        protected int iconMargins, iconLabelTextOffset = 0, iconLabelTopMargin = 0;
        private const int TEXTSIZESCALEFACTOR = 9, ICONMARGINSCALEFACTOR = 6;
        private float viewWidth = DimensionHelper.DpToPx(144), viewHeight = DimensionHelper.DpToPx(144), backgroundCircleRadius;


        protected IconView(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        public IconView(Context context) : base(context)
        {
            Initialize(context);
        }

        public IconView(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            Initialize(context, attrs);
        }

        private void Initialize(Context context, IAttributeSet attrs = null)
        {
            if (attrs != null)
            {
                // Contains the values set for the styleable attributes you declared in your attrs.xml
                var array = context.ObtainStyledAttributes(attrs, Resource.Styleable.IconView, 0, 0);

                iconBackgroundColor = array.GetColor(Resource.Styleable.IconView_bg_color, Color.Gray);
                iconLabelTextColor = array.GetColor(Resource.Styleable.IconView_iconLabelTextColor, Color.ParseColor("#D9000000"));
                _labelText = array.GetString(Resource.Styleable.IconView_iconLabelText);
                _showIconLabel = array.GetBoolean(Resource.Styleable.IconView_showIconLabel, false);

                var iconResId = array.GetResourceId(Resource.Styleable.IconView_src, 0);
                if (iconResId != 0) // If the user actually set a drawable
                    _icon = AppCompatDrawableManager.Get().GetDrawable(context, iconResId);

                // If the users sets text for the icon without setting the showIconLabel attr to true
                // set it to true for the user anyways
                if (_labelText != null)
                    _showIconLabel = true;

                // Very important to recycle the array after use
                array.Recycle();
            }

            iconBackgroundPaint = new Paint(PaintFlags.AntiAlias) { Color = iconBackgroundColor };
            iconLabelTextColorPaint = new Paint(PaintFlags.AntiAlias)
            {
                Color = iconLabelTextColor,
                TextSize = iconLabelTextSize,
                TextAlign = Paint.Align.Center
            };
        }

        protected override void OnDraw(Canvas canvas)
        {
            if (viewWidth == 0 || viewHeight == 0)
            {
                return;
            }

            if (_showIconLabel)
            {
                DrawIconLabel(canvas);
            }

            DrawBackgroundCircle(canvas);

            if (_icon != null)
            {
                DrawIcon(canvas);
            }
        }

        private void DrawBackgroundCircle(Canvas canvas)
        {
            canvas.DrawCircle(viewWidth / 2, (viewHeight / 2) - iconLabelTopMargin, backgroundCircleRadius, iconBackgroundPaint);
        }

        protected override void OnMeasure(int widthMeasureSpec, int heightMeasureSpec)
        {
            base.OnMeasure(widthMeasureSpec, heightMeasureSpec);
        }

        private void DrawIconLabel(Canvas canvas)
        {
            iconLabelTopMargin = (int)DimensionHelper.DpToPx(16);
            iconLabelTextOffset = (int)(iconLabelTextSize + iconLabelTopMargin + iconLabelBottomMargin);

            canvas.DrawText(_labelText, viewWidth / 2, viewHeight - iconLabelBottomMargin, iconLabelTextColorPaint);
        }

        private void DrawIcon(Canvas canvas)
        {
            int leftBounds = iconMargins + iconLabelTextOffset;
            int topBounds = iconMargins + iconLabelTextOffset + iconLabelTopMargin;
            int rightBounds = (int)(viewWidth - iconMargins - iconLabelTextOffset); ;
            int bottomBounds = 0;

            _icon.SetBounds(leftBounds, topBounds, rightBounds, bottomBounds);
            _icon.Draw(canvas);
        }



        public static class DimensionHelper
        {
            public static float DpToPx(float dpValue)
            {
                var metrics = Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity.Resources.DisplayMetrics;
                return (int)TypedValue.ApplyDimension(ComplexUnitType.Dip, dpValue, metrics);
            }

            public static float SpToPx(float spValue)
            {
                var metrics = Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity.Resources.DisplayMetrics;
                return (int)TypedValue.ApplyDimension(ComplexUnitType.Sp, spValue, metrics);
            }
        }

    }
}