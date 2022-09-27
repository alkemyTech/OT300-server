using OngProject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.DataAccess.Seeds
{
    public static class UserSeed
    {
        public static User[] GetData()
        {
            var users = new[] {
                new User()
                {
                    Id = 1,
                    FirstName = "Marta",
                    LastName = "Juarez",
                    Email = "MartaJuarez@gmail.com",
                    Password = "123456",
                    Photo = ""
                },

                new User()
                {
                    Id = 2,
                    FirstName = "Marcos",
                    LastName = "Perez",
                    Email = "Marcosperez@gmail.com",
                    Password = "1234789",
                    Photo = ""
                },
                new User()
                {
                    Id = 3,
                    FirstName = "Carlos",
                    LastName = "Juarez",
                    Email = "CarlosJuarez@gmail.com",
                    Password = "123456",
                    Photo = ""
                },
                new User()
                {
                    Id = 4,
                    FirstName = "Marta",
                    LastName = "Juarez",
                    Email = "MartaJuarez@gmail.com",
                    Password = "123456",
                    Photo = ""
                },
                new User()
                {
                    Id = 5,
                    FirstName = "Carlos",
                    LastName = "Casarez",
                    Email = "CarlosJuarez@gmail.com",
                    Password = "123456",
                    Photo = ""
                },
                new User()
                {
                    Id = 6,
                    FirstName = "Marta",
                    LastName = "Juarez",
                    Email = "MartaJuarez@gmail.com",
                    Password = "123456",
                    Photo = ""
                },
                new User()
                {
                    Id = 7,
                    FirstName = "Marcela",
                    LastName = "Torrez",
                    Email = "MarcelaTorrez@gmail.com",
                    Password = "123456",
                    Photo = "data:image/png;base64,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"
                },
                new User()
                {
                    Id = 8,
                    FirstName = "Maria",
                    LastName = "Juarez",
                    Email = "MariaJuarez@gmail.com",
                    Password = "12345678",
                    Photo = "data:image/png;base64,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"
                },
                new User()
                {
                    Id = 9,
                    FirstName = "andrea",
                    LastName = "torres",
                    Email = "andreaTorres@gmail.com",
                    Password = "123456",
                    Photo = "data:image/png;base64,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"
                },
                new User()
                {
                    Id = 10,
                    FirstName = "Graciela",
                    LastName = "Canzian",
                    Email = "GracielaCanzian@gmail.com",
                    Password = "1234",
                    Photo = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAHMAAABzCAMAAACcnc3UAAABBVBMVEX////I7f+U1PMAAAAAGDCw5v8ARWYAO1zK7/+R0O/M8f+X2PgAFS75+fnP9P8hISGKxuMAQWPk5ORun7aRkZGJiYkAKlPT+f/A5/h3q8Tx8fEAABoqKiqCu9YALlUAOV0hMDfLy8uZmZlhYWFDQ0M0NDS0tLRZWVl/f39OcYIRERHa2toySFIoOkK17P+mpqZijaJDYG5ra2tylqm22OYAMU8AAB86VGCn2fEbJyylydiCqLgjUnBYf5GdzOIAAA5SfpsAJD8VJjkeM0UAFEUAIUxCZ3+Nusy99f+Yt8JcbHYAHkF+mKNxiI8ABykxXnyAjZzT5vCTnakxSWSyu8RMXXVqe42+HdxlAAALQUlEQVRoge2aaVva2hbHD4OSAUJCrIxJRJCCMgoKx4K2WrB6Wrz3DP3+H+XueUxwaPvqnvXCxweS/cta67/W3jvs33771/6vrdTsHHdb7ZNGo3HSbnWPO83Sr8QVmqNW8TSj2mmxNWoWfgWw1GkVNRy3Yqvzk/0tdN7r/mn+vu/8PG9LBw1x7N8H/fmkB20y7w9+F79qHPwcZ0td7uJRvzeuBoHrmthcNwiq417/iDvb/XFqqcuGG/T8AMDSqgF04PcG7LofpBaOqY9HPT+OJ3D9HvX29PgH8tpsk1H64yCZx7jBuE+ubzff6uQ7MsIcuPgsEVFdf07uefcmV8+Ik/2XEgmV+No+ez2yQ/I41okwr0i2+F+VOiZ57bwWSeI6UfIIQYu9Cre9BfpMzuuExPd1yBauft9V/FtUDMsAZjnQ8L9GZaH46/q4U7ReQSzgVPYVJxdg/BQwwzGuPkyn0w9XhmOhDyxjobiKs9p+sZIKJ+iGnuikuahgYCplVVZhGEW5KArD3hWmAmxlIT6h20NjnLwQSpBjUySmKDHlXB16+9S88OmqbBBqSqSa49dAcWB9U/SRAg2nfHW4L5p3uHLYt6Kvpo/D+xJkS0NWLDJmqjybTve9fdkij19gVTToC4T0TkGaC8NgyOnnKLevmbe+cuglhsFdJdBnS6ajIrkPqfIq1IHIBKjoKoE+0xzOFPmYLJMAOY4SkDLUEKBYSDvbIJZsj+ckxZHOhyQvoYUzHg8jxXPae1a8KJlzWpdmOsXNUvSq5nRpGcLVafrY7vyZlDZRwwvikIa13IUE6p06qThogNpg4nyKW57PAyuYM6XJ9BLg4ZUlQan5O5vgsZRMUxzBmPFkmoGnlih6lo3ETLFxUEqP45EluPY5YsmsiPlxVrQuI9e23f2YKlUc5ep14Xx6Gr8w64qRlZHczci392zb3ES6q4qjHIqi241DotKc0+sW0v0sm96TvbcHoHaw8cDUIoPFeoFKp/ObOU8qUuRmlV5mSI9sUN2EAWRCqG2C1fRmKfqrSDdlUAeqCY6W0FLEjIssqE0a2iVCEiqwdPBRyO3SkB+VRtdEixU9o+9EN+XIppwxGTfn7wmGse5H1hKVcuHRrcY2hlpRyKbipmE8kQiG5p5mtu1HScFljsKMFmsKsyOKdmHItxLVep431JnAANTDypVvTLFlkh83v7wHnw3cmG4AQ/SAHPGW0cd45nATLTG0okBpZ3DhFuq9oiDYD1bkoa7lrJB0Rv6Xez+eueeC7+BzHSoJTVnXZMyV3hc6goJM9WGtDXTC27PjgTirX6Cn0Qc5oTyjVT24MLR9mnBLvW+JIjtMJ0PT9vAjYObGCjNl0UH7anCRaklozYUqhBlO59BOZtr2EGrb26hMtjxaqcptSqFVb5uh2dpb7GZi4aZUEUnBFafRY2FGUUNLu1B0v5N5jdrGUhMuDS6aXcQZDaZznhBaViqbYTqZuYcXaHqxsODOlYSidNrxqgWLL9xnovtk5dqLkEwtGpME10YJFapTTKd6E2MCaEJ57g2v2XSm3W6ICeUViiREll6umk5hKRQtv8Qz7+ma8FBnWkQngSyikSChxQ7m/jqh9330djAXgohGjHnAO4IuIZGZEN0vbIEWw6QiQl3hgDG7gmw1CYlMoN3Y0Ea7mBVBuHyxAEullyRbriE45iIutE9sgRKnISrcnlQsbbFU1Hskphc3m13ztW+kM1NisfAdcGMnk6+GoCPXGnS4ZG56eh/iTLhHa0gtYYyZrnYL7fHElipzOBXCsNFUD8zlzOILmWT+pNHT5m0xCur8+Xam6GhUVRqgzRXkPRl6aBOYjd1MabsLlCtCwQTHGoLnzeJCKzJ5Ptu7WjyCPnjU1dwQzlzMSWDVEAPDp1ikEa9boT7dWGbKmU2X+x7YKES9IfZOsDX42NvfPFixXhoVN64+z2Ef2uUnfBnlzGZXM6CSIfeQGPhwNquUrYQ7qZ+wD50z5rtdUza/2ShPw7VWn7a9iUIrAZhSJm2+fxDmFVNd3HKznNnn3FprfkBEUbQpO0lQ69qMm1fQ/OkmzZ8kttbD4x+53OdYZi7sf6048XfS+dOV50+03Q12FItVnn27vNnmEpm5i3pm/uDEOkskFMgb34LYFHQRWeWHfmabz38CzLW2M4P5BF/ks9uby6+GRmWyRS1BeH0CC3Rix4sIEG8z22w2m78AQx9qs7ZtPwGml4VXXGa+VcrKpo5IyJ5k5BercKEwIPIKlP3uQ/0mn0UGkLlwqjND+MUndFF+m3mU+59F1lnmQFomkC1SEBdcY0aJ2bwHhta3g7a5hswLclX25ns5LrSBuklCW0GaUGnaLT9e0sGyMLY5bdsLeh/8+BO9Kn8rPrQxE9IpbwbRvowkNHDEx7zNM2Y+FydcJFsSWmT1r8IAFvntwtb2Zfi1G13hCo46X+tZblBFoTJ/LuycjMzmB5bmJl7dyi/gUIWS4IqtyBmIg8FiiZ5kph0citmEl9X5bps2IRRa9bUUrJZ+mjwTS4j1UFeQObX74dBK0O2Eqsio0Bez/Yz+E8QIbVlUR8uPWz4URirVYtsR/vhCyHuWZoe5iTYrI4VZKvK2wBw1ZlkhtDlqQxGJVAuFe6GriLmJGkJRexHWFUrUvMeOOh9EBRE/c2veimzchJCJGR1gpnVvCsWpv/BDcwtzFAen3M+LI13gwSO2gYDvN9c6Mpu9RCoyZqKbMe/HCy3BUdwAjdmlNFKeOnpNiTZRkFwsgPmtLLQ97GYr5vW45CiKrvNVZtKUhv+x7DR6q7n4b6gpCD3drcUjm+gmcPRckC6IrpVyjvIKk0q3VqiAZdjC+O3PMA4JVAQWZCyySLTnsb8CIEcHaVak1pXqJk3p4Z/wGcEghb/wQ6jI7OX3MivN9CDJTTAAku6KRjfQQ0uh0V/kof9e6/ohwTXoj8RoXZvpJvzYUToRZWRe321jBsMq+hvf8U+k6wdfdlmlSCSgk8TzIGgaPaIhMf28PhjWUfSIrj/7HJtM2HL5z3xH+ttF0WrnonbjobjrrtFzfw9jk5nN5xkSa/ZcfUUtRrchzC9J0AtULvARk5KZZ7+f4vmksfOkDf7Jtcqg1bt4HSEVIQXpyby8qzIkKpNnfnQtoMmb6Qiod17XoDCln4H2H6O4ZNbn7FgD1k/muaM9OKUZl0LT7qquyxcFtxbGILf1FTvWYKOl+85kEmhbgZrVfl0ZGegIBBeGVv2i3mdxpcj2s0hapTy8wNXxrUIFKV2Xvke5TwrxdszPbpDAJlemaGdFWUgwqyuFms8djv6JJP3k69mVcECFyKf4wmNEBDrmUEi9u9mKiGi5Ly5Htjd3IpEUyYuRAIrP803SEtWf3AJsnqX0QgDeTnyJmMZHiBqvOCxVwkI6EpIK8xr4m/5t/RKAgX3Kwr/by/r2buMH0kEjO8BHpdqvOnVXwiWTWaUlqmkGgb/a9O9uITB7e9efwEOG8oklG/2uAYvklQf9agf4vkHVlqjoKB88vQgNn2iUvwYrQXK88OAFRSJboUMOaU4CBbrLwKKMHAZrvOXAaOGMxDfTC1Rfk4k9cs/52dvOMtZG9Dzq5CVU7mOmMXp1XLmr7IRof+zuxNq2y05OZrpvdJK42mzRgU7nY9e0Y8DwN3x3PGcnZlvNNztJqR1GBSqeTAME5ma6wXTCD8JmWp0fJWJfz8WT1KeDOSjKse/743FvMh+Ih56L5z/sI6OeHbczz1v7+OxnEaEVSs2D9q5j3Kftg2bppx9YL9Sao24s97TdHTVrv+SEPNwu1M46o4PzVvukCO2k3To/GHXOaoVfBOTkGrASNPjPr6b9a6+y/wH8mXMgMqXYAgAAAABJRU5ErkJggg=="
                }
            };

            foreach (var u in users)
            {
                u.CreatedAt = DateTime.Now;
                u.IsDeleted = false;
                u.LastEditedAt = DateTime.Now;
            }

            return users;
        }
    }
}
