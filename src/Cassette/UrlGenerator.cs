using System;
using Cassette.Utilities;

namespace Cassette
{
    class UrlGenerator : IUrlGenerator
    {
        readonly IUrlModifier urlModifier;

        public UrlGenerator(IUrlModifier urlModifier)
        {
            this.urlModifier = urlModifier;
        }

        public string CreateBundleUrl(Bundle bundle)
        {
            return urlModifier.Modify(bundle.Url);
        }

        public string CreateAssetUrl(IAsset asset)
        {
            // "~/directory/file.js" --> "cassette.axd/asset/directory/file.js?hash"
            // Asset URLs are only used in debug mode. The hash is placed in the querystring, not the path.
            // This maintains the asset directory structure i.e. two assets in the same directory appear together in web browser JavaScript development tooling.
            
            var assetPath = asset.Path.Substring(1);
            var hash = asset.Hash.ToUrlSafeBase64String();
            var url = "asset" + assetPath + "?" + hash;

            return urlModifier.Modify(url);
        }

        public string CreateRawFileUrl(string filename, string hash)
        {
            if (filename.StartsWith("~") == false)
            {
                throw new ArgumentException("Image filename must be application relative (starting with '~').");
            }

            var path = ConvertToForwardSlashes(filename).Substring(1);
            var url = "file/" + hash + path;
            
            return urlModifier.Modify(url);
        }

        public string CreateAbsolutePathUrl(string applicationRelativePath)
        {
            var url = applicationRelativePath.TrimStart('~', '/');
            return urlModifier.Modify(url);
        }

        string ConvertToForwardSlashes(string path)
        {
            return path.Replace('\\', '/');
        }
    }
}