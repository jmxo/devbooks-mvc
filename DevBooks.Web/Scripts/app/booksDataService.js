var httpVerbs = {
    POST: 'POST',
    PUT: 'PUT',
    GET: 'GET',
    DEL: 'DELETE'
};

var booksDataService = (function () {

    var
        ds = {
            commit: function (type, url, data) {

                // Remove 'id' member to perpare for INSERT
                if (type === httpVerbs.POST) {
                    delete data['id'];
                }

                return $.ajax({
                    type: type,
                    url: url,
                    data: data,
                    dataType: 'json'
                });
            },

            del: function (data) {
                return this.commit(httpVerbs.DEL, '/api/books/' + data.id);
            },

            save: function (data) {

                var
                    type = httpVerbs.POST,
                    url = '/api/books';

                if (data.id > 0) {
                    type = httpVerbs.PUT;
                    url += '/' + data.id;
                }

                return this.commit(type, url, data);
            },

            saveImage: function (data) {
                return $.ajax({
                    type: httpVerbs.POST,
                    url: '/books/uploadimage',
                    processData: false,
                    contentType: false,
                    data: data
                });
            },
            
        };

    _.bindAll(ds, 'del', 'save'); //make del and save functions run under the ds object, this helps with the context of "this" inside the functions

    return {
        save: ds.save,
        del: ds.del,
        saveImage: ds.saveImage
    }

})();