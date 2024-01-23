using AutoMapper;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using RequestManager.API.Dto;
using RequestManager.API.Handlers.GoodsHandler;
using RequestManager.API.Handlers.SendHandler;
using RequestManager.API.SendHandler;

namespace RequestManager.Client.Pages;

public partial class SendGoodsTable
{
    private List<SendGoodsDto> Send { get; set; }
    private int _page = 1;
    private int _pageSize = 10;
    private int _totalItems = 0;
    private IEnumerable<GoodsDto> Goods { get; set; }

    [Inject] private IMapper Maper { get; set; }

    private bool _canCancelEdit = true;
    private bool _blockSwitch = false;
    private DateTime? _dateFrom = DateTime.MinValue;
    private DateTime? _datebefore = DateTime.UtcNow;
    private bool _isFirst = true;
    private SendGoodsDto _selectedItem = null;
    private SendGoodsDto _elementBeforeEdit;
    private HashSet<SendGoodsDto> _selectedItems;
    private TableApplyButtonPosition _applyButtonPosition = TableApplyButtonPosition.End;
    private TableEditButtonPosition _editButtonPosition = TableEditButtonPosition.End;
    private TableEditTrigger _editTrigger = TableEditTrigger.EditButton;

    [Inject] private GetRequestsSendGoodsHandler GetRequestsHandler { get; set; }

    [Inject] private GetRequestsGoodsHandler GetGoodsHandler { get; set; }

    [Inject] private AddSendGoodsHandler AddGoodsHandler { get; set; }

    [Inject] private EditSendHandler EditRequestHandler { get; set; }
    [Inject] private DeleteSendRequestHandler DeleteRequestHandler { get; set; }

    private MudTable<SendGoodsDto> _mudTable;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            Send = (await GetRequestsHandler.Handle(new GetRequestsSendGoods(_dateFrom, _datebefore))).RequestDto.ToList();
            _selectedItems = new();
            Goods = (await GetGoodsHandler.Handle(new GetRequestsGoods(0))).RequestDto.ToList();

            await InvokeAsync(StateHasChanged);
        }
        await base.OnAfterRenderAsync(firstRender);
    }

    private async Task<TableData<SendGoodsDto>> LoadPage(TableState state)
    {
        _isFirst = true;
        _page = state.Page;
        _pageSize = state.PageSize;

        var response = await GetRequestsHandler.Handle(new GetRequestsSendGoods(_dateFrom, _datebefore, _page, _pageSize));
        Send = response.RequestDto.ToList();

        _totalItems = (await GetRequestsHandler.Handle(new GetRequestsSendGoods(_dateFrom, _datebefore, _page))).Count;

        await InvokeAsync(StateHasChanged);
        return new TableData<SendGoodsDto>() { TotalItems = _totalItems, Items = Send };
    }

    private void BackupItem(SendGoodsDto element)
    {
        _selectedItem = _elementBeforeEdit;
    }

    public async void Drop()
    {
        foreach (var request in _selectedItems)
        {
            Send.Remove(request);
            await DeleteRequestHandler.Handle(new DeleteRequestSendGoods(request));
        }
        await InvokeAsync(StateHasChanged);
    }

    private async void ItemHasBeenCommitted(SendGoodsDto element)
    {
        try
        {
            if (element.Id == 0)
            {
                element.Id = (await GetRequestsHandler.Handle(new GetRequestsSendGoods(_dateFrom, _datebefore, 0))).RequestDto.Last().Id;
                await EditRequestHandler.Handle(new EditRequestSendGoods(element));
                Send = (await GetRequestsHandler.Handle(new GetRequestsSendGoods(_dateFrom, _datebefore))).RequestDto.ToList();
            }
            else
            {
                await EditRequestHandler.Handle(new EditRequestSendGoods(element));
            }

            await InvokeAsync(StateHasChanged);
        }
        catch
        {
        }
    }

    private async void ResetItemToOriginalValues(SendGoodsDto element)
    {
        if (element.Id == 0)
        {
            await DeleteRequestHandler.Handle(new DeleteRequestSendGoods(element));
        }
        else
        {
            _elementBeforeEdit = Maper.Map<SendGoodsDto>(element);
        }
    }

    public async void Filter()
    {
        if (!_isFirst)
        {
            //var response = await GetRequestsHandler.Handle(new GetRequestsSendGoods(_dateFrom, _datebefore, _page, _pageSize));
            await _mudTable.ReloadServerData();
        }
        else
        {
            _isFirst = false;
        }
    }

    private async void AddRecord()
    {
        _mudTable.SetEditingItem(null);
        var newRecord = new SendGoodsDto
        {
            Id = 0,
            SendDate = DateTime.UtcNow,
            Count = 0
        };

        Send.Insert(0, newRecord);

        await AddGoodsHandler.Handle(new AddRequestSendGoods(newRecord));

        _mudTable.SetSelectedItem(Send.First());
        _mudTable.SetEditingItem(Send.First());

        await InvokeAsync(StateHasChanged);
    }
}