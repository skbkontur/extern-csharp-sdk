using System.IO;
using System.Text;
using Kontur.Extern.Client.ApiLevel.Json;
using Kontur.Extern.Client.Benchmarks.JsonBenchmarks.JsonNetAdapters;
using Kontur.Extern.Client.Http.Serialization;
using Kontur.Extern.Client.Models.Docflows;

namespace Kontur.Extern.Client.Benchmarks.JsonBenchmarks
{
    public class JsonConvertersBenchmarkContext
    {
        public const int OperationsPerInvoke = 100;
        private const string json = @"{
  ""id"": ""0c536ce5-bdc0-c93b-c47e-74789b111982"",
  ""organization-id"": ""5bce1a2c-8b53-6566-dbd2-09df80f220e8"",
  ""type"": ""urn:docflow:business-registration"",
  ""documents"": [
    {
      ""id"": ""8f88437e-cb97-5023-a9dd-728a898fdf08"",
      ""description"": {
        ""filename"": ""beauty__health_\u0026_computers_vortals.sfd_hdstx"",
        ""content-type"": ""Monitored"",
        ""compressed"": false,
        ""requisites"": {},
        ""related-docflows-count"": 2013387996793010667,
        ""support-recognition"": true
      },
      ""content"": {
        ""decrypted"": {
          ""href"": ""http://aurore.name"",
          ""rel"": ""Rustic"",
          ""name"": ""London"",
          ""title"": ""payment"",
          ""profile"": ""TCP"",
          ""templated"": true
        },
        ""encrypted"": {
          ""href"": ""https://amari.info"",
          ""rel"": ""pixel"",
          ""name"": ""Amely"",
          ""title"": ""Credit Card Account"",
          ""profile"": ""SMS"",
          ""templated"": true
        }
      },
      ""send-date"": ""2021-08-26T03:49:17.1700828+02:00"",
      ""signatures"": [
        {
          ""id"": ""7e475150-eefb-965e-b43a-e93c8abf0d4a"",
          ""title"": ""Springs"",
          ""signature-certificate-thumbprint"": ""Home Loan Account"",
          ""content-link"": {
            ""href"": ""http://faustino.name"",
            ""rel"": ""Generic"",
            ""name"": ""Jacquelyn"",
            ""title"": ""Extension"",
            ""profile"": ""lime"",
            ""templated"": false
          },
          ""links"": [
            {
              ""href"": ""http://lea.biz"",
              ""rel"": ""Connecticut"",
              ""name"": ""Roscoe"",
              ""title"": ""Seamless"",
              ""profile"": ""Coordinator"",
              ""templated"": true
            },
            {
              ""href"": ""https://breana.info"",
              ""rel"": ""Jewelery"",
              ""name"": ""Eldred"",
              ""title"": ""deposit"",
              ""profile"": ""Soft"",
              ""templated"": false
            },
            {
              ""href"": ""https://darlene.biz"",
              ""rel"": ""back-end"",
              ""name"": ""Jayda"",
              ""title"": ""salmon"",
              ""profile"": ""pink"",
              ""templated"": false
            }
          ]
        },
        {
          ""id"": ""ca3b1da2-627a-84c1-7aec-aa7a34bddc68"",
          ""title"": ""Communications"",
          ""signature-certificate-thumbprint"": ""Accountability"",
          ""content-link"": {
            ""href"": ""https://sigmund.name"",
            ""rel"": ""Personal Loan Account"",
            ""name"": ""Elise"",
            ""title"": ""withdrawal"",
            ""profile"": ""Fantastic Rubber Bacon"",
            ""templated"": true
          },
          ""links"": [
            {
              ""href"": ""http://easton.org"",
              ""rel"": ""Mississippi"",
              ""name"": ""Jerrod"",
              ""title"": ""Auto Loan Account"",
              ""profile"": ""Generic Rubber Tuna"",
              ""templated"": false
            },
            {
              ""href"": ""https://wilson.info"",
              ""rel"": ""withdrawal"",
              ""name"": ""Gayle"",
              ""title"": ""Borders"",
              ""profile"": ""IB"",
              ""templated"": false
            },
            {
              ""href"": ""http://sanford.info"",
              ""rel"": ""Squares"",
              ""name"": ""Virginie"",
              ""title"": ""quantifying"",
              ""profile"": ""sexy"",
              ""templated"": true
            }
          ]
        },
        {
          ""id"": ""55142c08-5f24-0a4b-2ed6-095e23e2ac31"",
          ""title"": ""Solutions"",
          ""signature-certificate-thumbprint"": ""New Taiwan Dollar"",
          ""content-link"": {
            ""href"": ""https://kaylie.name"",
            ""rel"": ""context-sensitive"",
            ""name"": ""Estefania"",
            ""title"": ""Engineer"",
            ""profile"": ""pricing structure"",
            ""templated"": true
          },
          ""links"": [
            {
              ""href"": ""http://earnestine.com"",
              ""rel"": ""3rd generation"",
              ""name"": ""Enid"",
              ""title"": ""CFA Franc BEAC"",
              ""profile"": ""program"",
              ""templated"": true
            },
            {
              ""href"": ""http://shane.com"",
              ""rel"": ""Buckinghamshire"",
              ""name"": ""Roosevelt"",
              ""title"": ""Realigned"",
              ""profile"": ""ivory"",
              ""templated"": false
            },
            {
              ""href"": ""http://norwood.com"",
              ""rel"": ""primary"",
              ""name"": ""Ursula"",
              ""title"": ""Sharable"",
              ""profile"": ""compress"",
              ""templated"": false
            }
          ]
        }
      ],
      ""links"": [
        {
          ""href"": ""http://samson.info"",
          ""rel"": ""initiative"",
          ""name"": ""Holly"",
          ""title"": ""Personal Loan Account"",
          ""profile"": ""application"",
          ""templated"": false
        },
        {
          ""href"": ""https://cortney.org"",
          ""rel"": ""Rustic"",
          ""name"": ""Horacio"",
          ""title"": ""Tunisia"",
          ""profile"": ""override"",
          ""templated"": true
        },
        {
          ""href"": ""https://henriette.name"",
          ""rel"": ""port"",
          ""name"": ""Jordyn"",
          ""title"": ""Nebraska"",
          ""profile"": ""copying"",
          ""templated"": false
        }
      ]
    },
    {
      ""id"": ""97bd86cb-6d82-002d-76a9-ec246e293404"",
      ""description"": {
        ""filename"": ""mountain_jbod.rms"",
        ""content-type"": ""interface"",
        ""compressed"": false,
        ""requisites"": {},
        ""related-docflows-count"": -8972819510298655205,
        ""support-recognition"": false
      },
      ""content"": {
        ""decrypted"": {
          ""href"": ""https://lillie.com"",
          ""rel"": ""focus group"",
          ""name"": ""Ernie"",
          ""title"": ""auxiliary"",
          ""profile"": ""24 hour"",
          ""templated"": false
        },
        ""encrypted"": {
          ""href"": ""https://christian.info"",
          ""rel"": ""Steel"",
          ""name"": ""Addie"",
          ""title"": ""Virtual"",
          ""profile"": ""distributed"",
          ""templated"": true
        }
      },
      ""send-date"": ""2021-08-25T17:14:20.7522326+02:00"",
      ""signatures"": [
        {
          ""id"": ""09a02841-1a8c-058d-4787-b79352a04224"",
          ""title"": ""bluetooth"",
          ""signature-certificate-thumbprint"": ""Seamless"",
          ""content-link"": {
            ""href"": ""http://hassan.org"",
            ""rel"": ""optical"",
            ""name"": ""Dorothy"",
            ""title"": ""Frozen"",
            ""profile"": ""Rupiah"",
            ""templated"": false
          },
          ""links"": [
            {
              ""href"": ""https://theron.info"",
              ""rel"": ""Awesome Rubber Hat"",
              ""name"": ""Brannon"",
              ""title"": ""Garden, Outdoors \u0026 Jewelery"",
              ""profile"": ""Refined Cotton Pants"",
              ""templated"": false
            },
            {
              ""href"": ""http://johathan.name"",
              ""rel"": ""auxiliary"",
              ""name"": ""Emmanuel"",
              ""title"": ""Integration"",
              ""profile"": ""synthesize"",
              ""templated"": true
            },
            {
              ""href"": ""https://troy.net"",
              ""rel"": ""synergize"",
              ""name"": ""Cruz"",
              ""title"": ""engineer"",
              ""profile"": ""array"",
              ""templated"": true
            }
          ]
        },
        {
          ""id"": ""91461849-83ac-cfdd-1c22-8974ba984060"",
          ""title"": ""Metal"",
          ""signature-certificate-thumbprint"": ""THX"",
          ""content-link"": {
            ""href"": ""http://ellis.net"",
            ""rel"": ""Planner"",
            ""name"": ""Anne"",
            ""title"": ""Brunei Dollar"",
            ""profile"": ""synthesize"",
            ""templated"": false
          },
          ""links"": [
            {
              ""href"": ""http://odie.name"",
              ""rel"": ""Shoes \u0026 Computers"",
              ""name"": ""Waldo"",
              ""title"": ""quantifying"",
              ""profile"": ""Incredible Plastic Tuna"",
              ""templated"": true
            },
            {
              ""href"": ""https://dion.info"",
              ""rel"": ""pixel"",
              ""name"": ""Mackenzie"",
              ""title"": ""invoice"",
              ""profile"": ""vortals"",
              ""templated"": true
            },
            {
              ""href"": ""https://turner.info"",
              ""rel"": ""Interactions"",
              ""name"": ""Celia"",
              ""title"": ""Falls"",
              ""profile"": ""policy"",
              ""templated"": true
            }
          ]
        },
        {
          ""id"": ""050dd3f9-b1bd-156e-ba60-e1ec7d3742af"",
          ""title"": ""International"",
          ""signature-certificate-thumbprint"": ""Springs"",
          ""content-link"": {
            ""href"": ""https://elyssa.name"",
            ""rel"": ""Frozen"",
            ""name"": ""Ciara"",
            ""title"": ""Administrator"",
            ""profile"": ""index"",
            ""templated"": false
          },
          ""links"": [
            {
              ""href"": ""https://herbert.net"",
              ""rel"": ""Kenyan Shilling"",
              ""name"": ""Jordy"",
              ""title"": ""Planner"",
              ""profile"": ""Islands"",
              ""templated"": false
            },
            {
              ""href"": ""https://daron.com"",
              ""rel"": ""Burundi"",
              ""name"": ""Rosetta"",
              ""title"": ""e-tailers"",
              ""profile"": ""Director"",
              ""templated"": false
            },
            {
              ""href"": ""https://jabari.name"",
              ""rel"": ""payment"",
              ""name"": ""Antwan"",
              ""title"": ""Face to face"",
              ""profile"": ""withdrawal"",
              ""templated"": true
            }
          ]
        }
      ],
      ""links"": [
        {
          ""href"": ""https://esmeralda.biz"",
          ""rel"": ""Executive"",
          ""name"": ""Axel"",
          ""title"": ""Home Loan Account"",
          ""profile"": ""overriding"",
          ""templated"": false
        },
        {
          ""href"": ""https://pat.info"",
          ""rel"": ""hacking"",
          ""name"": ""Era"",
          ""title"": ""Hawaii"",
          ""profile"": ""programming"",
          ""templated"": false
        },
        {
          ""href"": ""https://miles.net"",
          ""rel"": ""Unbranded Soft Table"",
          ""name"": ""Reese"",
          ""title"": ""hacking"",
          ""profile"": ""Niger"",
          ""templated"": true
        }
      ]
    },
    {
      ""id"": ""609b0b90-6728-0b38-54dd-af0a76bc7183"",
      ""description"": {
        ""filename"": ""kuwaiti_dinar.ini"",
        ""content-type"": ""Intelligent"",
        ""compressed"": true,
        ""requisites"": {},
        ""related-docflows-count"": -5865598514885400832,
        ""support-recognition"": false
      },
      ""content"": {
        ""decrypted"": {
          ""href"": ""https://leonard.net"",
          ""rel"": ""system"",
          ""name"": ""Frederique"",
          ""title"": ""Home Loan Account"",
          ""profile"": ""Dominican Republic"",
          ""templated"": true
        },
        ""encrypted"": {
          ""href"": ""https://cleora.org"",
          ""rel"": ""benchmark"",
          ""name"": ""Jonathan"",
          ""title"": ""Handmade Steel Mouse"",
          ""profile"": ""Administrator"",
          ""templated"": true
        }
      },
      ""send-date"": ""2021-08-25T21:12:59.5092764+02:00"",
      ""signatures"": [
        {
          ""id"": ""07b5b400-56e7-30ba-c486-c7512d00c3c4"",
          ""title"": ""communities"",
          ""signature-certificate-thumbprint"": ""Summit"",
          ""content-link"": {
            ""href"": ""https://floy.biz"",
            ""rel"": ""parse"",
            ""name"": ""Consuelo"",
            ""title"": ""innovate"",
            ""profile"": ""withdrawal"",
            ""templated"": false
          },
          ""links"": [
            {
              ""href"": ""https://tillman.biz"",
              ""rel"": ""program"",
              ""name"": ""Stone"",
              ""title"": ""bypassing"",
              ""profile"": ""attitude"",
              ""templated"": false
            },
            {
              ""href"": ""http://clotilde.info"",
              ""rel"": ""bi-directional"",
              ""name"": ""Brielle"",
              ""title"": ""Mississippi"",
              ""profile"": ""Cambridgeshire"",
              ""templated"": true
            },
            {
              ""href"": ""https://maci.org"",
              ""rel"": ""Sleek Soft Towels"",
              ""name"": ""Isaias"",
              ""title"": ""New Hampshire"",
              ""profile"": ""Program"",
              ""templated"": true
            }
          ]
        },
        {
          ""id"": ""47661022-4f2c-d31a-60b3-29f965fac9b0"",
          ""title"": ""Haven"",
          ""signature-certificate-thumbprint"": ""Generic Fresh Shirt"",
          ""content-link"": {
            ""href"": ""https://natalia.name"",
            ""rel"": ""Operations"",
            ""name"": ""Brendan"",
            ""title"": ""Industrial \u0026 Books"",
            ""profile"": ""application"",
            ""templated"": true
          },
          ""links"": [
            {
              ""href"": ""http://charley.info"",
              ""rel"": ""Generic Frozen Keyboard"",
              ""name"": ""Carli"",
              ""title"": ""Crescent"",
              ""profile"": ""fuchsia"",
              ""templated"": false
            },
            {
              ""href"": ""https://baron.org"",
              ""rel"": ""Chief"",
              ""name"": ""Reyna"",
              ""title"": ""deliverables"",
              ""profile"": ""Personal Loan Account"",
              ""templated"": true
            },
            {
              ""href"": ""http://clare.com"",
              ""rel"": ""JBOD"",
              ""name"": ""Sophie"",
              ""title"": ""frame"",
              ""profile"": ""didactic"",
              ""templated"": false
            }
          ]
        },
        {
          ""id"": ""c6314658-a868-f426-60c6-7427a0821068"",
          ""title"": ""Technician"",
          ""signature-certificate-thumbprint"": ""Consultant"",
          ""content-link"": {
            ""href"": ""https://dallin.net"",
            ""rel"": ""salmon"",
            ""name"": ""Aiyana"",
            ""title"": ""hardware"",
            ""profile"": ""Unbranded"",
            ""templated"": true
          },
          ""links"": [
            {
              ""href"": ""https://xzavier.net"",
              ""rel"": ""Wooden"",
              ""name"": ""Jairo"",
              ""title"": ""didactic"",
              ""profile"": ""withdrawal"",
              ""templated"": false
            },
            {
              ""href"": ""http://dave.name"",
              ""rel"": ""unleash"",
              ""name"": ""Elza"",
              ""title"": ""Platinum"",
              ""profile"": ""back up"",
              ""templated"": true
            },
            {
              ""href"": ""https://pablo.com"",
              ""rel"": ""connecting"",
              ""name"": ""Alessandro"",
              ""title"": ""copy"",
              ""profile"": ""Guyana Dollar"",
              ""templated"": true
            }
          ]
        }
      ],
      ""links"": [
        {
          ""href"": ""https://arthur.com"",
          ""rel"": ""Afghani"",
          ""name"": ""Arianna"",
          ""title"": ""ubiquitous"",
          ""profile"": ""programming"",
          ""templated"": true
        },
        {
          ""href"": ""https://salvatore.org"",
          ""rel"": ""Ford"",
          ""name"": ""Remington"",
          ""title"": ""navigate"",
          ""profile"": ""HTTP"",
          ""templated"": true
        },
        {
          ""href"": ""http://joany.info"",
          ""rel"": ""Ranch"",
          ""name"": ""Jarrett"",
          ""title"": ""fresh-thinking"",
          ""profile"": ""ivory"",
          ""templated"": false
        }
      ]
    }
  ],
  ""links"": [
    {
      ""href"": ""https://kailyn.biz"",
      ""rel"": ""Jewelery, Books \u0026 Jewelery"",
      ""name"": ""Emily"",
      ""title"": ""withdrawal"",
      ""profile"": ""Licensed"",
      ""templated"": false
    },
    {
      ""href"": ""https://jett.org"",
      ""rel"": ""Investor"",
      ""name"": ""Dewitt"",
      ""title"": ""Junctions"",
      ""profile"": ""circuit"",
      ""templated"": false
    },
    {
      ""href"": ""http://adriana.net"",
      ""rel"": ""Diverse"",
      ""name"": ""Tillman"",
      ""title"": ""unleash"",
      ""profile"": ""pixel"",
      ""templated"": false
    }
  ],
  ""send-date"": ""2021-08-25T20:15:26.265186+02:00"",
  ""last-change-date"": ""2021-08-25T15:04:52.2819749+02:00"",
  ""description"": {
    ""recipient"": ""connecting"",
    ""sender-inn"": ""7704646193"",
    ""svd-reg-codes"": [
      ""Awesome Frozen Computer"",
      ""South Dakota"",
      ""Internal""
    ],
    ""registration-info"": {
      ""applicant-infos"": [
        {
          ""fio"": {
            ""surname"": ""Conn"",
            ""name"": ""Glennie"",
            ""patronymic"": ""Marcella""
          },
          ""inn"": ""1869872729""
        },
        {
          ""fio"": {
            ""surname"": ""Robel"",
            ""name"": ""Angel"",
            ""patronymic"": ""Aaliyah""
          },
          ""inn"": ""9536760013""
        },
        {
          ""fio"": {
            ""surname"": ""Howe"",
            ""name"": ""Stephania"",
            ""patronymic"": ""Christine""
          },
          ""inn"": ""3629931160""
        }
      ],
      ""business-type"": 1,
      ""ul-info"": {
        ""name"": ""Efren""
      }
    },
    ""original-draft-id"": ""886e2ba0-6d7d-b412-d8c9-b60b8d9e52a6""
  }
}";

        private readonly byte[] utf8JsonBytes;
        private readonly byte[] emptyJsonBytes;

        public JsonConvertersBenchmarkContext()
        {
            SysSerializer = JsonSerializerFactory.CreateJsonSerializer(true);
            JsonNetSerializer = JsonNetSerializerFactory.CreateJsonSerializer(true);
            Docflow = SysSerializer.Deserialize<IDocflowWithDocuments>(json);
            utf8JsonBytes = Encoding.UTF8.GetBytes(json);
            emptyJsonBytes = new byte[json.Length*sizeof (char)];
        }

        public IJsonSerializer JsonNetSerializer { get; }
        public IJsonSerializer SysSerializer { get; }
        public IDocflowWithDocuments Docflow { get; }
        public string Json => json;
        public Stream JsonStream => new MemoryStream(utf8JsonBytes);
        public byte[] JsonBytes => utf8JsonBytes;
        public Stream TargetStream => new MemoryStream(emptyJsonBytes);
    }
}