using Bunit;
using DropdownTest.Layout;
using Microsoft.AspNetCore.Components;
using Xunit;

namespace DropdownUnitTest
{
    public class DropdownUnitTesting : TestContext
    {

        public DropdownUnitTesting()
        {
            // Configuration the JavaScript Interop for la a call bcDropdown.initialize
            JSInterop.SetupVoid("bcDropdown.initialize", _ => true);
            JSInterop.SetupVoid("bcDropdown.collapse", _ => true);
        }

        [Fact]
        public void ComponentRendersCorrectly()
        {
            // Arrange
            var items = new List<MyItem>
        {
            new MyItem { Id = 1, Name = "Zboncakmouth" },
            new MyItem { Id = 2, Name = "Langton" },
            // Add more
        };


            var componentParameters = new ComponentParameter[]
            {
        ComponentParameter.CreateParameter(nameof(BCDropdown<MyItem, int>.Items), items),
        ComponentParameter.CreateParameter(nameof(BCDropdown<MyItem, int>.ItemValue), (Func<MyItem, int>)(item => item.Id)),
        ComponentParameter.CreateParameter(nameof(BCDropdown<MyItem, int>.ItemText), (Func<MyItem, string>)(item => item.Name)),
        ComponentParameter.CreateParameter(nameof(BCDropdown<MyItem, int>.ElementId), "test-dropdown")
            };

            // Act
            var component = RenderComponent<BCDropdown<MyItem, int>>(componentParameters);

            // Assert
            component.MarkupMatches(@"<div class=""dropdown "" id=""test-dropdown"" >
              <a class=""form-select"" role=""button"" tabindex=""0"" data-bs-toggle=""dropdown"" aria-expanded=""false"" id=""dropdown-select""  >
                <span class=""text-wrap"" >Select...</span>
              </a>
              <ul class=""dropdown-menu w-100 bg-light-subtle shadow "" id=""dropdown-list"" >
                <li class="""" >
                  <a class=""dropdown-item"" id=""search-box"" >
                    <input type=""search"" id=""dropdown-filter"" placeholder=""search..."" autocomplete=""off"" tabindex=""0"" class=""form-control form-control-sm border-secondary small"" value=""""  >
                  </a>
                </li>
                <li >
                  <a class=""dropdown-item text-muted small""  id=""item-0"" href=""javascript:;"" >(No selection)
                  </a>
                </li>
                <li  class="""" >
                  <a class=""dropdown-item py-0"" href=""javascript:;"" id=""item-1"" >
                    <span class=""my-1"" >Zboncakmouth</span>
                  </a>
                </li>
                <li  class="""" >
                  <a class=""dropdown-item py-0"" href=""javascript:;"" id=""item-2"" >
                    <span class=""my-1"" >Langton</span>
                  </a>
                </li>
              </ul>
            </div>
            <script >
                var bcDropdown = {};

                bcDropdown.initialize = (elemId) => {
                    console.log('initialize', elemId);

                    const element = document.getElementById(elemId);
                    const select = element.querySelector('#dropdown-select');
                    const list = element.querySelector('ul');
                    const searchContainer = element.querySelector('#search-box');
                    const input = element.querySelector('#search-box > input');
                    const item0 = element.querySelector('#item-0');

                    // console.log('element', element);
                    // console.log('select', select);
                    // console.log('list', list);
                    // console.log('input', input);
                    // console.log('item0', item0);

                    //bootstrap dropdown object
                    bootstrapComp = new bootstrap.Dropdown(element);
                
                    //set dropdown-expand event
                    element.addEventListener(""shown.bs.dropdown"", () => {
                        //console.log('on shown.bs.dropdown');
                        input.scrollIntoView(); 
                        list.scrollTop = 0; 
                    })

                    //focus search box on alphanumeric key 
                    list.addEventListener('keydown', (event) => {
                        if (document.activeElement != input && /^[a-zA-Z0-9]$/.test(event.key)) {
                            // console.log('on select ArrowDown');
                            //focus search
                            input.focus();
                        }
                    });
                    select.addEventListener('keydown', (event) => {
                        if (/^[a-zA-Z0-9]$/.test(event.key)) {
                            // console.log('on select alphanumeric keydown ');
                            //expand if not expanded
                            if (!list.checkVisibility()) {
                                list.classList.add(""show"");
                                //bootstrapComp.show();
                            }
                            //focus search
                            input.focus();
                        }
                    });

                    //set arrow navigation: select -> input -> item0 -> item 1 ...

                    select.addEventListener('keyup', (event) => {
                        // console.log('on select keyup:', event.key);
                        if (event.key == 'ArrowDown') {
                            input.focus();
                        }
                    });

                    input.addEventListener('keyup', (event) => {
                        if (event.key == 'ArrowUp') {
                            // console.log('on input ArrowUp');
                            select.focus();
                        } else if (event.key == 'ArrowDown') {
                            // console.log('on input ArrowDown');
                            item0.focus();
                        }
                    });

                    item0.addEventListener('keyup', (event) => {
                        if (event.key == 'ArrowUp') {
                            // console.log('on item0 ArrowUp keypress');
                            input.focus();
                        } 
                    });
                };

                bcDropdown.collapse = (elemId) => {
                    const element = document.getElementById(elemId);
                    const list = element.querySelector('ul');
                    //const bootstrapComp = new bootstrap.Dropdown(element);
                    //bootstrapComp.hide();
                    list.classList.remove('show');
                };


            </script>");
        }


    }
    public class MyItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}