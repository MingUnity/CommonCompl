﻿using System;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using UnityEngine;

namespace MingUnity.AssetBundles
{
    /// <summary>
    /// AB包加载
    /// </summary>
    public class AssetBundlesLoader : IAssetBundlesLoader
    {
        private Dictionary<string, AssetBundleTask> _taskDic = new Dictionary<string, AssetBundleTask>();

        private AssetBundleTask this[string key]
        {
            get
            {
                AssetBundleTask task = null;

                if (!string.IsNullOrEmpty(key))
                {
                    _taskDic?.TryGetValue(key, out task);
                }

                return task;
            }
            set
            {
                if (!string.IsNullOrEmpty(key))
                {
                    _taskDic[key] = value;
                }
            }
        }

        /// <summary>
        /// 获取资源
        /// </summary>
        public T GetAsset<T>(string abPath, string assetName) where T : UnityEngine.Object
        {
            T asset = null;

            AssetBundleTask task = GetTask(abPath);

            task?.Load(abPath, false, (ab) =>
            {
                asset = ab?.LoadAsset<T>(assetName);
            });

            return asset;
        }

        /// <summary>
        /// 异步获取资源
        /// </summary>
        public void GetAssetAsync<T>(string abPath, string assetName, Action<T> callback) where T : UnityEngine.Object
        {
            AssetBundleTask task = GetTask(abPath);

            task?.Load(abPath, true, (ab) =>
            {
                Task.CreateTask(GetAssetAsync(ab, assetName, (asset) =>
                  {
                      callback?.Invoke(asset as T);
                  }));
            });
        }

        /// <summary>
        /// 获取任务
        /// </summary>
        private AssetBundleTask GetTask(string abPath)
        {
            string abKey = GetAbKey(abPath);

            AssetBundleTask task = this[abKey];

            if (task == null)
            {
                task = new AssetBundleTask();

                this[abKey] = task;
            }

            return task;
        }

        /// <summary>
        /// 异步获取资源
        /// </summary>
        private IEnumerator GetAssetAsync(AssetBundle ab, string assetName, Action<UnityEngine.Object> callback)
        {
            if (ab != null)
            {
                AssetBundleRequest abReq = ab.LoadAssetAsync(assetName);

                yield return abReq;

                if (abReq.isDone)
                {
                    callback?.Invoke(abReq.asset);
                }
                else
                {
                    callback?.Invoke(null);
                }
            }
            else
            {
                callback?.Invoke(null);
            }
        }

        /// <summary>
        /// 卸载
        /// </summary>
        public void Unload(string abPath, bool unloadAllLoadedObjects)
        {
            string abKey = GetAbKey(abPath);

            AssetBundleTask task = this[abKey];

            if (task != null)
            {
                task.Unload(unloadAllLoadedObjects);

                _taskDic?.Remove(abKey);
            }
        }

        /// <summary>
        /// 卸载全部
        /// </summary>
        public void UnloadAll(bool unloadAllLoadedObjects)
        {
            if (_taskDic != null)
            {
                foreach (AssetBundleTask task in _taskDic.Values)
                {
                    task?.Unload(unloadAllLoadedObjects);
                }

                _taskDic.Clear();
            }
        }

        /// <summary>
        /// 获取AB包key值 
        /// </summary>
        private string GetAbKey(string abPath)
        {
            string key = string.Empty;

            if (!string.IsNullOrEmpty(abPath))
            {
                key = Path.GetFileName(abPath);
            }

            return key;
        }
    }
}