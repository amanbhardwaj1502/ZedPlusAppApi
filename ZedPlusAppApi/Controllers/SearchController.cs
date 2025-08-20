using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using ZedPlusAppApi.Models;

namespace ZedPlusAppApi.Controllers
{
    public class SearchController : ApiController
    {
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/GetSearchList")]
        public SearchResponse GetSearchList(string SearchItem)
        {

            SearchResponse resp = new SearchResponse();
            List<SearchVM> mdl = new List<SearchVM>();
            try
            {
                db_zedPlusShopEntities db = new db_zedPlusShopEntities();
                MatchCollection matches = Regex.Matches(SearchItem, @"\d+");

                List<int> numericValues = new List<int>();
                MatchCollection numberMatches = Regex.Matches(SearchItem, @"\d+");

                // Collect individual digits and combinations of digits
                foreach (Match match in numberMatches)
                {
                    // Convert to int and add
                    numericValues.Add(int.Parse(match.Value));
                }

                // Generate numeric combinations
                List<int> allNumericCombinations = GetNumericCombinations(SearchItem);
                numericValues = numericValues.Union(allNumericCombinations).ToList(); // Combine unique values

                // Extract character values
                List<string> characterValues = new List<string>();
                MatchCollection charMatches = Regex.Matches(SearchItem, "[a-zA-Z]");

                // Collect individual characters
                foreach (Match match in charMatches)
                {
                    characterValues.Add(match.Value);
                }

                // Generate character combinations
                List<string> allCharacterCombinations = GetCharacterCombinations(characterValues);
                characterValues = characterValues.Union(allCharacterCombinations).ToList();
                foreach (var items1 in numericValues)
                {
                    var result = (from tbl in db.tblItemMasters
                                  where tbl.Item_Name.Contains(items1.ToString()) || tbl.Item_Price == items1.ToString() || tbl.Item_Detail.Contains(items1.ToString()) || tbl.Item_PointValue.Contains(items1.ToString())
                                  select new
                                  {
                                      tbl.ItemID,
                                      tbl.Item_Image,
                                      tbl.Item_Name,

                                  }).ToList();
                    if (result.Count() > 0)
                    {
                        foreach (var list in result)
                        {
                            string image = "";
                            if (list.Item_Image != null)
                            {
                                if (list.Item_Image.Contains(','))
                                {
                                    image = list.Item_Image.Split(',')[0];
                                }
                                else { image = list.Item_Image; }
                            }

                            mdl.Add(new SearchVM
                            {
                                ItemId = list.ItemID,
                                CategoryId = "0",
                                SubCategoryId = "0",
                                ItemName = list.Item_Name,
                                ItemImage = image,

                            });
                        }
                        
                    }
                    else
                    {
                        resp = new SearchResponse { Status_Code = "0", Status = "error", Message = "Data Not Found" };
                    }
                }
                foreach (var items2 in characterValues)
                {
                    var result = (from tbl in db.tblItemMasters
                                  where tbl.Item_Name.Contains(items2) || tbl.Item_Detail.Contains(items2)
                                  select new
                                  {
                                      tbl.ItemID,
                                      tbl.Item_Image,
                                      tbl.Item_Name,

                                  }).ToList();
                    if (result.Count() > 0)
                    {
                        foreach (var list in result)
                        {
                            string image = "";
                            if (list.Item_Image != null)
                            {
                                if (list.Item_Image.Contains(','))
                                {
                                    image = list.Item_Image.Split(',')[0];
                                }
                                else { image = list.Item_Image; }
                            }

                            mdl.Add(new SearchVM
                            {
                                ItemId = list.ItemID,
                                CategoryId = "0",
                                SubCategoryId = "0",
                                ItemName = list.Item_Name,
                                ItemImage = image,

                            });
                        }
                        
                    }
                    else
                    {
                        resp = new SearchResponse { Status_Code = "0", Status = "error", Message = "Data Not Found" };
                    }
                    var result1 = (from tbl in db.tblCategoryMasters
                                   where tbl.Category_Name.Contains(items2)
                                   select new
                                   {
                                       tbl.CategoryID,
                                       tbl.Category_Name,
                                       tbl.CategoryImage,

                                   }).ToList();
                    if (result1.Count() > 0)
                    {
                        foreach (var list in result1)
                        {
                            string image = "";
                            if (list.CategoryImage != null)
                            {
                                if (list.CategoryImage.Contains(','))
                                {
                                    image = list.CategoryImage.Split(',')[0];
                                }
                                else { image = list.CategoryImage; }
                            }

                            mdl.Add(new SearchVM
                            {
                                ItemId = 0,
                                CategoryId = list.CategoryID.ToString(),
                                SubCategoryId = "0",
                                ItemName = list.Category_Name,
                                ItemImage = image,

                            });
                        }
                       
                    }
                    else
                    {
                        resp = new SearchResponse { Status_Code = "0", Status = "error", Message = "Data Not Found" };
                    }
                    var result2 = (from tbl in db.tblSubCategoryMasters
                                   where tbl.SubCategory_Name.Contains(items2)
                                   select new
                                   {
                                       tbl.SubCategoryID,
                                       tbl.SubCategory_Name,
                                       tbl.ImagePath,

                                   }).ToList();
                    if (result2.Count() > 0)
                    {
                        foreach (var list in result2)
                        {
                            string image = "";
                            if (list.ImagePath != null)
                            {
                                if (list.ImagePath.Contains(','))
                                {
                                    image = list.ImagePath.Split(',')[0];
                                }
                                else { image = list.ImagePath; }
                            }

                            mdl.Add(new SearchVM
                            {
                                ItemId = 0,
                                CategoryId = "0",
                                SubCategoryId = list.SubCategoryID.ToString(),
                                ItemName = list.SubCategory_Name,
                                ItemImage = image,

                            });
                        }
                        
                    }
                    else
                    {
                        resp = new SearchResponse { Status_Code = "0", Status = "error", Message = "Data Not Found" };
                    }
                }
                resp = new SearchResponse
                {
                    SearchList = mdl.OrderByDescending(x => x.ItemName.Equals(SearchItem, StringComparison.OrdinalIgnoreCase))  // Exact match
        .ThenByDescending(x => x.ItemName.StartsWith(SearchItem, StringComparison.OrdinalIgnoreCase))  // Starts with search term
        .ThenByDescending(x => x.ItemName.ToLower().Contains(SearchItem.ToLower()))  // Contains search term (case-insensitive)
        .Distinct()
        .ToList()
                };
                return resp;
            }
            catch (Exception ex)
            {
                resp = new SearchResponse { Status_Code = "0", Status = "error", Message = ex.Message };
            }
            return resp;
        }

        private static List<int> GetNumericCombinations(string input)
        {
            var numericMatches = Regex.Matches(input, @"\d+");
            List<int> combinations = new List<int>();

            if (numericMatches.Count > 0)
            {
                // Get all unique numeric values from the input
                var uniqueNumbers = new HashSet<int>();

                foreach (Match match in numericMatches)
                {
                    uniqueNumbers.Add(int.Parse(match.Value));
                }

                // Generate combinations of unique numbers
                string numStr = string.Join("", uniqueNumbers);
                for (int i = 0; i < numStr.Length; i++)
                {
                    for (int j = i + 1; j <= numStr.Length; j++)
                    {
                        int combination = int.Parse(numStr.Substring(i, j - i));
                        combinations.Add(combination);
                    }
                }

                // Add all permutations of numeric matches
                foreach (var number in uniqueNumbers)
                {
                    combinations.Add(number);
                }
            }

            return combinations.Distinct().ToList();
        }

        private static List<string> GetCharacterCombinations(List<string> characters)
        {
            var combinations = new HashSet<string>();

            // Generate combinations of characters
            int n = characters.Count;
            for (int i = 0; i < (1 << n); i++)
            {
                string temp = string.Empty;
                for (int j = 0; j < n; j++)
                {
                    if ((i & (1 << j)) > 0)
                    {
                        temp += characters[j];
                    }
                }
                if (temp.Length > 0)
                {
                    combinations.Add(temp);
                }
            }

            return combinations.ToList();
        }

    }
}