mergeInto(LibraryManager.library, {
  IsMobileBrowser: function () {
    return (/iPhone|iPad|iPod|Android/i.test(navigator.userAgent));
  },
  
  IsAndroidBrowser: function () {
      return (/Android/i.test(navigator.userAgent));
  },
  
  IsIosBrowser: function () {
      return (/iPhone|iPad|iPod/i.test(navigator.userAgent));
  },
})