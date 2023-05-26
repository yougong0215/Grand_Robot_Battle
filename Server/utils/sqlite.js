const sqlite3 = require('sqlite3').verbose();

exports.GetObject = function() {
    let db = new sqlite3.Database(Config.DBfile);
    
    // 동기식으로 해야징
    db.Aget = function(query, data) {
        return new Promise(reslove => {
            db.get(query, data, function(err, result) {
                if (err) {
                    console.error(err);
                    reslove();
                    return;
                }
                
                reslove(result);
            });
        });
    }

    db.Aall = function(query, data) {
        return new Promise(reslove => {
            db.all(query, data, function(err, result) {
                if (err) {
                    console.error(err);
                    reslove();
                    return;
                }
                
                reslove(result);
            });
        });
    }

    db.Arun = function(query, data) {
        return new Promise(reslove => {
            db.run(query, data, function(err) {
                if (err)
                    console.error(err);
                reslove(err ? undefined : true);
            });
        });
    }

    return db;
}