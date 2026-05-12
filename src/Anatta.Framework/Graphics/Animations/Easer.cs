using OpenTK.Mathematics;

namespace Anatta.Framework.Graphics.Animations;

internal class EasingHelper {
    public static Colour4 TweenValues(Colour4 startColour, Colour4 endColour, int runningTime, int startTime, int endTime,
        Easing easing) {
        if (startColour == endColour) {
            return startColour;
        }

        int num = runningTime - startTime;
        int num2 = endTime - startTime;
        if (num2 == 0 || num == 0) {
            return startColour;
        }

        return new Colour4(
            (byte)Math.Max(0.0,
                Math.Min(255.0, ApplyEasing(easing, num, (int)startColour.R, endColour.R - startColour.R, num2))),
            (byte)Math.Max(0.0,
                Math.Min(255.0, ApplyEasing(easing, num, (int)startColour.G, endColour.G - startColour.G, num2))),
            (byte)Math.Max(0.0,
                Math.Min(255.0, ApplyEasing(easing, num, (int)startColour.B, endColour.B - startColour.B, num2))),
            (byte)Math.Max(0.0,
                Math.Min(255.0, ApplyEasing(easing, num, (int)startColour.A, endColour.A - startColour.A, num2))));
    }

    internal static Vector2 TweenValuesv2(Vector2 val1, Vector2 val2, double runningTime, double startTime,
        float endTime, Easing easing) {
        float num = (float)(runningTime - startTime);
        float num2 = (float)((double)endTime - startTime);
        if (num2 == 0f || num == 0f) {
            return val1;
        }

        return new Vector2((float)ApplyEasing(easing, num, val1.X, val2.X - val1.X, num2),
            (float)ApplyEasing(easing, num, val1.Y, val2.Y - val1.Y, num2));
    }

    public static float TweenValuesf(float val1, float val2, int runningTime, int startTime, int endTime,
        Easing easing) {
        if (val1 == val2) {
            return val1;
        }

        int num = runningTime - startTime;
        int num2 = endTime - startTime;
        if (num == 0) {
            return val1;
        }

        if (num2 == 0) {
            return val2;
        }

        return (float)ApplyEasing(easing, num, val1, val2 - val1, num2);
    }

    internal static double ApplyEasing(Easing easing, double time, double initial, double change,
        double duration) {
        if (change == 0.0 || time == 0.0 || duration == 0.0) {
            return initial;
        }

        if (time == duration) {
            return initial + change;
        }

        switch (easing) {
            default:
                return change * (time / duration) + initial;
            case Easing.In:
            case Easing.InQuad:
                return change * (time /= duration) * time + initial;
            case Easing.Out:
            case Easing.OutQuad:
                return (0.0 - change) * (time /= duration) * (time - 2.0) + initial;
            case Easing.InOutQuad:
                if ((time /= duration / 2.0) < 1.0) {
                    return change / 2.0 * time * time + initial;
                }

                return (0.0 - change) / 2.0 * ((time -= 1.0) * (time - 2.0) - 1.0) + initial;
            case Easing.InCubic:
                return change * (time /= duration) * time * time + initial;
            case Easing.OutCubic:
                return change * ((time = time / duration - 1.0) * time * time + 1.0) + initial;
            case Easing.InOutCubic:
                if ((time /= duration / 2.0) < 1.0) {
                    return change / 2.0 * time * time * time + initial;
                }

                return change / 2.0 * ((time -= 2.0) * time * time + 2.0) + initial;
            case Easing.InQuart:
                return change * (time /= duration) * time * time * time + initial;
            case Easing.OutQuart:
                return (0.0 - change) * ((time = time / duration - 1.0) * time * time * time - 1.0) + initial;
            case Easing.InOutQuart:
                if ((time /= duration / 2.0) < 1.0) {
                    return change / 2.0 * time * time * time * time + initial;
                }

                return (0.0 - change) / 2.0 * ((time -= 2.0) * time * time * time - 2.0) + initial;
            case Easing.InQuint:
                return change * (time /= duration) * time * time * time * time + initial;
            case Easing.OutQuint:
                return change * ((time = time / duration - 1.0) * time * time * time * time + 1.0) + initial;
            case Easing.InOutQuint:
                if ((time /= duration / 2.0) < 1.0) {
                    return change / 2.0 * time * time * time * time * time + initial;
                }

                return change / 2.0 * ((time -= 2.0) * time * time * time * time + 2.0) + initial;
            case Easing.InSine:
                return (0.0 - change) * Math.Cos(time / duration * 1.5707963705062866) + change + initial;
            case Easing.OutSine:
                return change * Math.Sin(time / duration * 1.5707963705062866) + initial;
            case Easing.InOutSine:
                return (0.0 - change) / 2.0 * (Math.Cos(3.1415927410125732 * time / duration) - 1.0) + initial;
            case Easing.InExpo:
                return change * Math.Pow(2.0, 10.0 * (time / duration - 1.0)) + initial;
            case Easing.OutExpo:
                if (time != duration) {
                    return change * (0.0 - Math.Pow(2.0, -10.0 * time / duration) + 1.0) + initial;
                }

                return initial + change;
            case Easing.InOutExpo:
                if ((time /= duration / 2.0) < 1.0) {
                    return change / 2.0 * Math.Pow(2.0, 10.0 * (time - 1.0)) + initial;
                }

                return change / 2.0 * (0.0 - Math.Pow(2.0, -10.0 * (time -= 1.0)) + 2.0) + initial;
            case Easing.InCirc:
                return (0.0 - change) * (Math.Sqrt(1.0 - (time /= duration) * time) - 1.0) + initial;
            case Easing.OutCirc:
                return change * Math.Sqrt(1.0 - (time = time / duration - 1.0) * time) + initial;
            case Easing.InOutCirc:
                if ((time /= duration / 2.0) < 1.0) {
                    return (0.0 - change) / 2.0 * (Math.Sqrt(1.0 - time * time) - 1.0) + initial;
                }

                return change / 2.0 * (Math.Sqrt(1.0 - (time -= 2.0) * time) + 1.0) + initial;
            case Easing.InElastic: {
                if ((time /= duration) == 1.0) {
                    return initial + change;
                }

                double num5 = duration * 0.3;
                double num6 = change;
                double num7 = 1.70158;
                if (num6 < Math.Abs(change)) {
                    num6 = change;
                    num7 = num5 / 4.0;
                }
                else {
                    num7 = num5 / 6.2831854820251465 * Math.Asin(change / num6);
                }

                return 0.0 - num6 * Math.Pow(2.0, 10.0 * (time -= 1.0)) *
                    Math.Sin((time * duration - num7) * 6.2831854820251465 / num5) + initial;
            }
            case Easing.OutElastic: {
                if ((time /= duration) == 1.0) {
                    return initial + change;
                }

                double num = duration * 0.3;
                double num2 = change;
                double num3 = 1.70158;
                if (num2 < Math.Abs(change)) {
                    num2 = change;
                    num3 = num / 4.0;
                }
                else {
                    num3 = num / 6.2831854820251465 * Math.Asin(change / num2);
                }

                return num2 * Math.Pow(2.0, -10.0 * time) *
                    Math.Sin((time * duration - num3) * 6.2831854820251465 / num) + change + initial;
            }
            case Easing.OutElasticHalf: {
                if ((time /= duration) == 1.0) {
                    return initial + change;
                }

                double num16 = duration * 0.3;
                double num17 = change;
                double num18 = 1.70158;
                if (num17 < Math.Abs(change)) {
                    num17 = change;
                    num18 = num16 / 4.0;
                }
                else {
                    num18 = num16 / 6.2831854820251465 * Math.Asin(change / num17);
                }

                return num17 * Math.Pow(2.0, -10.0 * time) *
                    Math.Sin((0.5 * time * duration - num18) * 6.2831854820251465 / num16) + change + initial;
            }
            case Easing.OutElasticQuarter: {
                if ((time /= duration) == 1.0) {
                    return initial + change;
                }

                double num13 = duration * 0.3;
                double num14 = change;
                double num15 = 1.70158;
                if (num14 < Math.Abs(change)) {
                    num14 = change;
                    num15 = num13 / 4.0;
                }
                else {
                    num15 = num13 / 6.2831854820251465 * Math.Asin(change / num14);
                }

                return num14 * Math.Pow(2.0, -10.0 * time) *
                    Math.Sin((0.25 * time * duration - num15) * 6.2831854820251465 / num13) + change + initial;
            }
            case Easing.InOutElastic: {
                if ((time /= duration / 2.0) == 2.0) {
                    return initial + change;
                }

                double num10 = duration * 0.44999999999999996;
                double num11 = change;
                double num12 = 1.70158;
                if (num11 < Math.Abs(change)) {
                    num11 = change;
                    num12 = num10 / 4.0;
                }
                else {
                    num12 = num10 / 6.2831854820251465 * Math.Asin(change / num11);
                }

                if (time < 1.0) {
                    return -0.5 * (num11 * Math.Pow(2.0, 10.0 * (time -= 1.0)) *
                                   Math.Sin((time * duration - num12) * 6.2831854820251465 / num10)) + initial;
                }

                return num11 * Math.Pow(2.0, -10.0 * (time -= 1.0)) *
                    Math.Sin((time * duration - num12) * 6.2831854820251465 / num10) * 0.5 + change + initial;
            }
            case Easing.InBack: {
                double num9 = 1.70158;
                return change * (time /= duration) * time * ((num9 + 1.0) * time - num9) + initial;
            }
            case Easing.OutBack: {
                double num8 = 1.70158;
                return change * ((time = time / duration - 1.0) * time * ((num8 + 1.0) * time + num8) + 1.0) + initial;
            }
            case Easing.InOutBack: {
                double num4 = 1.70158;
                if ((time /= duration / 2.0) < 1.0) {
                    return change / 2.0 * (time * time * (((num4 *= 1.525) + 1.0) * time - num4)) + initial;
                }

                return change / 2.0 * ((time -= 2.0) * time * (((num4 *= 1.525) + 1.0) * time + num4) + 2.0) + initial;
            }
            case Easing.InBounce:
                return change - ApplyEasing(Easing.OutBounce, duration - time, 0.0, change, duration) + initial;
            case Easing.OutBounce:
                if ((time /= duration) < 0.36363636363636365) {
                    return change * (7.5625 * time * time) + initial;
                }

                if (time < 0.7272727272727273) {
                    return change * (7.5625 * (time -= 0.5454545454545454) * time + 0.75) + initial;
                }

                if (time < 0.9090909090909091) {
                    return change * (7.5625 * (time -= 0.8181818181818182) * time + 0.9375) + initial;
                }

                return change * (7.5625 * (time -= 21.0 / 22.0) * time + 63.0 / 64.0) + initial;
            case Easing.InOutBounce:
                if (time < duration / 2.0) {
                    return ApplyEasing(Easing.InBounce, time * 2.0, 0.0, change, duration) * 0.5 + initial;
                }

                return ApplyEasing(Easing.OutBounce, time * 2.0 - duration, 0.0, change, duration) * 0.5 +
                       change * 0.5 + initial;
        }
    }

    public static float Ease(Easing e, float t, float b, float c, float d) {
        t /= d;
        float s;

        switch (e) {
            case Easing.None: return b + c * t;

            case Easing.In:
            case Easing.InQuad: return c * t * t + b;

            case Easing.Out:
            case Easing.OutQuad: return -c * t * (t - 2) + b;

            case Easing.InOutQuad:
                if ((t *= 2) < 1) return c / 2 * t * t + b;
                return -c / 2 * ((t - 1) * (t - 3) - 1) + b;

            case Easing.InCubic: return c * t * t * t + b;

            case Easing.OutCubic: return c * ((t -= 1) * t * t + 1) + b;

            case Easing.InOutCubic:
                if ((t *= 2) < 1) return c / 2 * t * t * t + b;
                t -= 2;
                return c / 2 * (t * t * t + 2) + b;

            case Easing.InQuart: return c * t * t * t * t + b;

            case Easing.OutQuart: return -c * ((t -= 1) * t * t * t - 1) + b;

            case Easing.InOutQuart:
                if ((t *= 2) < 1) return c / 2 * t * t * t * t + b;
                t -= 2;
                return -c / 2 * (t * t * t * t - 2) + b;

            case Easing.InQuint: return c * t * t * t * t * t + b;

            case Easing.OutQuint: return c * ((t -= 1) * t * t * t * t + 1) + b;

            case Easing.InOutQuint:
                if ((t *= 2) < 1) return c / 2 * t * t * t * t * t + b;
                t -= 2;
                return c / 2 * (t * t * t * t * t + 2) + b;

            case Easing.InSine: return -c * (float)Math.Cos(t * Math.PI / 2) + c + b;

            case Easing.OutSine: return c * (float)Math.Sin(t * Math.PI / 2) + b;

            case Easing.InOutSine: return -c / 2 * ((float)Math.Cos(Math.PI * t) - 1) + b;

            case Easing.InExpo:
                return (t == 0) ? b : c * (float)Math.Pow(2, 10 * (t - 1)) + b;

            case Easing.OutExpo:
                return (t == 1) ? b + c : c * (-(float)Math.Pow(2, -10 * t) + 1) + b;

            case Easing.InOutExpo:
                if (t == 0) return b;
                if (t == 1) return b + c;
                if ((t *= 2) < 1) return c / 2 * (float)Math.Pow(2, 10 * (t - 1)) + b;
                return c / 2 * (-(float)Math.Pow(2, -10 * (t - 1)) + 2) + b;

            case Easing.InCirc: return -c * ((float)Math.Sqrt(1 - t * t) - 1) + b;

            case Easing.OutCirc: return c * (float)Math.Sqrt(1 - (t - 1) * (t - 1)) + b;

            case Easing.InOutCirc:
                if ((t *= 2) < 1) return -c / 2 * ((float)Math.Sqrt(1 - t * t) - 1) + b;
                t -= 2;
                return c / 2 * ((float)Math.Sqrt(1 - t * t) + 1) + b;

            case Easing.InElastic:
                if (t == 0) return b;
                if (t == 1) return b + c;
                return -(c * (float)Math.Pow(2, 10 * (t -= 1)) *
                         (float)Math.Sin((t - 0.075f) * (2 * Math.PI) / 0.3f)) + b;

            case Easing.OutElastic:
                if (t == 0) return b;
                if (t == 1) return b + c;
                return c * (float)Math.Pow(2, -10 * t) *
                    (float)Math.Sin((t - 0.075f) * (2 * Math.PI) / 0.3f) + c + b;

            case Easing.OutElasticHalf:
                t *= 2;
                if (t < 1)
                    return c / 2 * (float)Math.Pow(2, -10 * t)
                                 * (float)Math.Sin((t - 0.075f) * (2 * Math.PI) / 0.3f) + b + c * .5f;
                return b + c;

            case Easing.OutElasticQuarter:
                t *= 4;
                if (t < 1)
                    return c / 4 * (float)Math.Pow(2, -10 * t)
                                 * (float)Math.Sin((t - 0.075f) * (2 * Math.PI) / 0.3f) + b + c * .75f;
                return b + c;

            case Easing.InOutElastic:
                if (t == 0) return b;
                if (t == 1) return b + c;
                if ((t *= 2) < 1)
                    return -c / 2 * (float)Math.Pow(2, 10 * (t - 1)) *
                        (float)Math.Sin((t - .1125f) * (2 * Math.PI) / .45f) + b;

                return c / 2 * (float)Math.Pow(2, -10 * (t - 1)) *
                    (float)Math.Sin((t - .1125f) * (2 * Math.PI) / .45f) + c + b;

            case Easing.InBack:
                s = 1.70158f;
                return c * t * t * ((s + 1) * t - s) + b;

            case Easing.OutBack:
                s = 1.70158f;
                t -= 1;
                return c * (t * t * ((s + 1) * t + s) + 1) + b;

            case Easing.InOutBack:
                s = 1.70158f * 1.525f;
                if ((t *= 2) < 1)
                    return c / 2 * (t * t * ((s + 1) * t - s)) + b;
                t -= 2;
                return c / 2 * (t * t * ((s + 1) * t + s) + 2) + b;

            case Easing.OutBounce:
                if (t < (1 / 2.75f)) return c * (7.5625f * t * t) + b;
                if (t < (2 / 2.75f)) return c * (7.5625f * (t -= (1.5f / 2.75f)) * t + .75f) + b;
                if (t < (2.5f / 2.75f)) return c * (7.5625f * (t -= (2.25f / 2.75f)) * t + .9375f) + b;
                return c * (7.5625f * (t -= (2.625f / 2.75f)) * t + .984375f) + b;

            case Easing.InBounce:
                return c - Ease(Easing.OutBounce, d - t * d, 0, c, d) + b;

            case Easing.InOutBounce:
                if (t < .5f)
                    return Ease(Easing.InBounce, t * 2, 0, c, d) * .5f + b;
                return Ease(Easing.OutBounce, t * 2 - 1, 0, c, d) * .5f + c * .5f + b;

            default:
                return b + c * t;
        }
    }
}