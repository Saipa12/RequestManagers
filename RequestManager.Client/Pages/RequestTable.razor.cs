using AutoMapper;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using RequestManager.API.Dto;
using RequestManager.API.GoodsHandler;
using RequestManager.API.Handlers.GoodsHandler;
using System.Reflection;

namespace RequestManager.Client.Pages;

public partial class RequestTable
{
    private List<GoodsDto> Goods { get; set; }
    private int _page = 1;
    private int _pageSize = 10;
    private int _totalItems = 0;
    //private IEnumerable<SendGoodsDto> Sends { get; set; }
    //private IEnumerable<DeliverGoodsDto> Delivs { get; set; }

    [Inject] private IMapper Maper { get; set; }

    private bool _canCancelEdit = true;
    private bool _blockSwitch = false;
    private string _searchString = "";
    private GoodsDto _selectedItem = null;
    private GoodsDto _elementBeforeEdit;
    private HashSet<GoodsDto> _selectedItems;
    private TableApplyButtonPosition _applyButtonPosition = TableApplyButtonPosition.End;
    private TableEditButtonPosition _editButtonPosition = TableEditButtonPosition.End;
    private TableEditTrigger _editTrigger = TableEditTrigger.EditButton;

    //[Inject] private IDialogService DialogService { get; set; }
    [Inject] private GetRequestsGoodsHandler GetRequestsHandler { get; set; }

    //[Inject] private GetRequestDeliverGoods GetDelivHandler { get; set; }
    //[Inject] private GetRequestSendHandler GetRequestSendHandler { get; set; }
    [Inject] private AddGoodsHandler AddGoodsHandler { get; set; }

    [Inject] private EditRequestGoodsHandler EditRequestHandler { get; set; }
    [Inject] private DeleteGoodsHandler DeleteRequestHandler { get; set; }

    private MudTable<GoodsDto> _mudTable;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            Goods = (await GetRequestsHandler.Handle(new GetRequestsGoods())).RequestDto.ToList();
            _selectedItems = new();
            //Delivs = (await GetDelivHandler.Handle(new GetDeliverRequests(true))).DeliverDto;
            //// _statuses = Enum.GetValues(typeof(RequestStatus)).Cast<RequestStatus>();

            await InvokeAsync(StateHasChanged);
        }
        await base.OnAfterRenderAsync(firstRender);
    }

    private async Task<TableData<GoodsDto>> LoadPage(TableState state)
    {
        _page = state.Page;
        _pageSize = state.PageSize;

        // Загрузите данные для текущей страницы, используя пагинацию
        var response = await GetRequestsHandler.Handle(new GetRequestsGoods(_page, _pageSize));
        Goods = response.RequestDto.ToList();
        // await InvokeAsync(StateHasChanged);
        // Вычислите общее количество элементов, возможно, также из серверного ответа
        _totalItems = (await GetRequestsHandler.Handle(new GetRequestsGoods(_page))).Count;
        return new TableData<GoodsDto>() { TotalItems = _totalItems, Items = Goods };
        //await InvokeAsync(StateHasChanged);
    }

    private void BackupItem(GoodsDto element)
    {
        _elementBeforeEdit = Maper.Map<GoodsDto>(element);
    }

    //public async void RejectRequest(GoodsDto request)
    //{
    //    //var parameters = new DialogParameters<ReasonDialog> { };

    //    var dialog = await DialogService.ShowAsync<ReasonDialog>($"{request.Id} From {request.DispatchAddress} To {request.DeliveryAddress} "/*, parameters*/);
    //    var result = await dialog.Result;

    //    if (!result.Canceled)
    //    {
    //        await RejectedRequestHandler.Handle(new RejectedRequest(request, result.Data.ToString()));
    //    }
    //}
    public void OpenMap()
    {
        Navigation.NavigateTo("send");
    }

    public async void Drop()
    {
        foreach (var request in _selectedItems)
        {
            //if (request.Status == RequestStatus.New)
            // {
            Goods.Remove(request);
            //  request.Deliver = null;
            await DeleteRequestHandler.Handle(new DeleteRequestGoods(request));
            //}
            //else
            //{
            //    RejectRequest(request);
            //}
        }
        await InvokeAsync(StateHasChanged);
    }

    private async void ItemHasBeenCommitted(GoodsDto element)
    {
        if (element.Id == 0)
        {
            element.Id = (await GetRequestsHandler.Handle(new GetRequestsGoods(0))).RequestDto.Last().Id;
            await EditRequestHandler.Handle(new EditRequestGoods(element));
            Goods = (await GetRequestsHandler.Handle(new GetRequestsGoods())).RequestDto.ToList();
        }
        else
        {
            await EditRequestHandler.Handle(new EditRequestGoods(element));
        }

        await InvokeAsync(StateHasChanged);
    }

    private async void ResetItemToOriginalValues(GoodsDto element)
    {
        if (element.Id == 0)
        {
            await DeleteRequestHandler.Handle(new DeleteRequestGoods(element));
        }
        else
        {
            _elementBeforeEdit = Maper.Map<GoodsDto>(element);
        }
    }

    private bool FilterFunc(GoodsDto element)
    {
        if (string.IsNullOrWhiteSpace(_searchString))
            return true;
        if (ConcatenateFields(element).Contains(_searchString, StringComparison.OrdinalIgnoreCase))
            return true;
        return false;
    }

    public static string ConcatenateFields(object obj)
    {
        // Получаем все поля класса
        var fields = obj.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

        // Создаем пустую строку
        var result = string.Empty;

        // Объединяем значения полей в строку
        foreach (var field in fields)
        {
            // Если поле не является статическим, то добавляем его значение к результату
            if (!field.IsStatic)
            {
                result += field.GetValue(obj)?.ToString();
            }
        }

        return result;
    }

    private async void AddRecord()
    {
        _mudTable.SetEditingItem(null);
        var newRecord = new GoodsDto
        {
            Surname = ""
            //DeliveryAddress = "",
            //DispatchAddress = "",
            //DeliveryDate = DateTime.UtcNow,
            //DeliveryTime = DateTime.UtcNow.AddDays(3)
        };

        Goods.Insert(0, newRecord);
        await AddGoodsHandler.Handle(new AddRequestGoods(newRecord));
        _mudTable.SetSelectedItem(Goods.First());
        _mudTable.SetEditingItem(Goods.First());
        await InvokeAsync(StateHasChanged);
    }
}