var data = (function (parent) {
    const moreDataUrl = '/Data/GetMoreData';
    const loadedDataSelector = '.loaded-data';
    var allRecordAreLoaded = false;
    var loading = false;

    function initScrollLoading() {
        var footerHeight = 100;

        $(window).scroll(function () {
            if (($(window).scrollTop() + $(window).height()) >= ($(document).height() - footerHeight) && !loading && !allRecordAreLoaded) {
                loadData(moreDataUrl);
            }
        });
    }

    function loadData(url) {
        const loadingBarSelector = '#loading-bar';

        loading = true;
        $(loadingBarSelector).text('Loading...');

        var nextPage = (+$(loadedDataSelector).attr('data-page')) + 1;

        $.post(
            url,
            {
                page: nextPage
            },
            function (response) {
                var $loader = $(loadingBarSelector);
                if (response) {
                    if (!response.datas.length) {
                        $loader.text('No results');
                        $loader.show();
                        return;
                    }

                    var template = $('#data-template').html();
                    var templateScript = Handlebars.compile(template);
                    var html = templateScript({
                        datas: response.datas
                    });
                    var $dataContainer = $(loadedDataSelector);

                    $loader.text('Load More');
                    $dataContainer.append(html);
                    $dataContainer.attr('data-page', nextPage);

                    var loadedRecordsCount = +parseInt($dataContainer.attr('data-loaded-records-count')) + response.datas.length;

                    allRecordAreLoaded = response.totalRecords <= loadedRecordsCount;

                    $dataContainer.attr('data-loaded-records-count', loadedRecordsCount);

                    if (allRecordAreLoaded) {
                        $loader.hide();
                    }
                    else {
                        $loader.show();
                    }
                }

                loading = false;
            }
        );
    }

    parent.main = {
        onReady: function () {
            initScrollLoading();
        }
    };

    return parent;

}(data || {}));