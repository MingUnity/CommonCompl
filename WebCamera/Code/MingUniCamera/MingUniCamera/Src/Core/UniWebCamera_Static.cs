﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MingUnity.WebCamera
{
    public partial class UniWebCamera
    {
        /// <summary>
        /// 网络摄像头列表
        /// </summary>
        private static Dictionary<int, UniWebCamera> _webCamDic = new Dictionary<int, UniWebCamera>();

        /// <summary>
        /// 初始化
        /// </summary>
        public static void Initialize(Action<bool> callback = null)
        {
            if (Application.HasUserAuthorization(UserAuthorization.WebCam))
            {
                callback?.Invoke(true);
            }
            else
            {
                TaskManager.CreateTask(AuthorizeRequest(callback));
            }
        }

        /// <summary>
        /// 网络摄像头数目
        /// </summary>
        public static int Count
        {
            get
            {
                int res = 0;

                WebCamDevice[] devices = WebCamTexture.devices;

                if (devices != null)
                {
                    res = devices.Length;
                }

                return res;
            }
        }

        /// <summary>
        /// 获取网络摄像头
        /// </summary>
        public static UniWebCamera Get(int index)
        {
            UniWebCamera result = null;

            _webCamDic?.TryGetValue(index, out result);

            if (result == null)
            {
                result = new UniWebCamera(index);

                if (_webCamDic != null)
                {
                    _webCamDic[index] = result;
                }
            }

            return result;
        }

        /// <summary>
        /// 获取网络摄像头
        /// </summary>
        public static UniWebCamera Get(string name)
        {
            UniWebCamera result = null;

            for (int i = 0; i < Count; i++)
            {
                UniWebCamera webCam = Get(i);

                if (webCam != null && webCam.Name == name)
                {
                    result = webCam;

                    break;
                }
            }

            return result;
        }

        /// <summary>
        /// 获取前置网络摄像头
        /// </summary>
        public static UniWebCamera GetFrontFacing()
        {
            UniWebCamera result = null;

            for (int i = 0; i < Count; i++)
            {
                UniWebCamera webCam = Get(i);

                if (webCam != null && webCam.IsFrontFacing)
                {
                    result = webCam;

                    break;
                }
            }

            return result;
        }

        /// <summary>
        /// 请求授权
        /// </summary>
        private static IEnumerator AuthorizeRequest(Action<bool> callback = null)
        {
            yield return Application.RequestUserAuthorization(UserAuthorization.WebCam);

            callback?.Invoke(Application.HasUserAuthorization(UserAuthorization.WebCam));
        }
    }
}
