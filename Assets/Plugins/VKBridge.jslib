var VKBridge = 
{
	BuyItemMethod: function(product_id)
    {
        buy_item(Pointer_stringify(product_id));
    },
	
	VKWebAppJoinGroupMethod: function(group_id)
	{
		VKWebAppJoinGroupMethod(group_id);
	},
	
	VKWebAppCheckNativeAdsMethod: function()
	{
		VKWebAppCheckNativeAdsMethod();
	},
	
	VKWebAppShowNativeAdsRewardMethod: function()
	{
		VKWebAppShowNativeAdsRewardMethod();
	},
	
	VKWebAppShowNativeAdsInterstitialMethod: function()
	{
		ShowInterstitialIndexMethod();
	},
	
	BrowserGetLinkHREF : function()
	{
		// https://css-tricks.com/snippets/javascript/get-url-and-url-parts-in-javascript/
		var search = window.location.href;
		var searchLen = lengthBytesUTF8(search) + 1;
		var buffer = _malloc(searchLen);
		stringToUTF8(search, buffer, searchLen);
		return  buffer;
	},
	
	GetQueryParam: function(paramId) 
	{
        var urlParams = new URLSearchParams(location.search);
        var param = urlParams.get(Pointer_stringify(paramId));
        var bufferSize = lengthBytesUTF8(param) + 1;
        var buffer = _malloc(bufferSize);
        stringToUTF8(param, buffer, bufferSize);
        return buffer;
    }
}

mergeInto(LibraryManager.library, VKBridge);