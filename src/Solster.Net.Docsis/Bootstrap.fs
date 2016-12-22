
module Solster.Net.Bootstrap

open System.Net
open System.Net.Sockets

let receive = 
    use client = new UdpClient(IPEndPoint(IPAddress.Any, 0x44), EnableBroadcast = true, ExclusiveAddressUse = false)
    fun () -> (client.ReceiveAsync() |> Async.AwaitTask |> Async.RunSynchronously)
